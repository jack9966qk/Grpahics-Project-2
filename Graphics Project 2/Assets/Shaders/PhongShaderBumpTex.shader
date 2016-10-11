// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Original HLSL stub copyright (c) 2010-2012 SharpDX - Alexandre Mutel
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
// Adapted for COMP30019 by Jeremy Nicholson, 10 Sep 2012
// Adapted further by Chris Ewin, 23 Sep 2013
// Adapted for texture/normal mapping in Unity by Alex Zable, 02 Sep 2016

Shader "Unlit/PhongShaderBumpTex"
{
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#define MAX_LIGHTS 10

			#include "UnityCG.cginc"

			uniform float _AmbientCoeff;
			uniform float _DiffuseCoeff;
			uniform float _SpecularCoeff;
			uniform float _SpecularPower;

			uniform int _NumPointLights;
			uniform float3 _PointLightColors[MAX_LIGHTS];
			uniform float3 _PointLightPositions[MAX_LIGHTS];

			uniform sampler2D _MainTex;
			uniform sampler2D _NormalMapTex;

			struct vertIn
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
				float4 tangent : TANGENT;
				float2 uv : TEXCOORD0;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 worldVertex : TEXCOORD1;
				float3 worldNormal : TEXCOORD2;
				float3 worldTangent : TEXCOORD3;
				float3 worldBinormal : TEXCOORD4;
			};

			// Implementation of the vertex shader
			vertOut vert(vertIn v)
			{
				vertOut o;

				// Convert Vertex position and corresponding normal into world coords
				// Note that we have to multiply the normal by the transposed inverse of the world 
				// transformation matrix (for cases where we have non-uniform scaling; we also don't
				// care about the "fourth" dimension, because translations don't affect the normal) 
				float4 worldVertex = mul(unity_ObjectToWorld, v.vertex);
				float3 worldNormal = normalize(mul(transpose((float3x3)unity_WorldToObject), v.normal.xyz));
				float3 worldTangent = normalize(mul(transpose((float3x3)unity_WorldToObject), v.tangent.xyz));
				float3 worldBinormal = normalize(cross(worldTangent, worldNormal));

				// Transform vertex in world coordinates to camera coordinates, and pass colour
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;

				// Pass out the world vertex position and world normal to be interpolated
				// in the fragment shader (and utilised)
				o.worldVertex = worldVertex;
				o.worldNormal = worldNormal;
				o.worldTangent = worldTangent;
				o.worldBinormal = worldBinormal;

				return o;
			}

			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
				// Sample colour from texture (i.e. pixel colour before lighting applied)
				float4 surfaceColor = tex2D(_MainTex, v.uv);

				// Modify normal based on normal map (and bring into range -1 to 1)
				float3 bump = (tex2D(_NormalMapTex, v.uv) - float3(0.5, 0.5, 0.5)) * 2.0;
				float3 bumpNormal = (bump.x * normalize(v.worldTangent)) + 
									(bump.y * normalize(v.worldBinormal)) + 
									(bump.z * normalize(v.worldNormal));
				bumpNormal = normalize(bumpNormal);

				// Calculate ambient RGB intensities
				float Ka = _AmbientCoeff; // (May seem inefficient, but compiler will optimise)
				float3 amb = surfaceColor * UNITY_LIGHTMODEL_AMBIENT.rgb * Ka;

				// Sum up lighting calculations for each light (only diffuse/specular; ambient does not depend on the individual lights)
				float3 dif_and_spe_sum = float3(0.0, 0.0, 0.0);
				for (int i = 0; i < _NumPointLights; i++)
				{
					// Calculate diffuse RBG reflections, we save the results of L.N because we will use it again
					// (when calculating the reflected ray in our specular component)
					float fAtt = 1;
					float Kd = _DiffuseCoeff;
					float3 L = normalize(_PointLightPositions[i] - v.worldVertex.xyz);
					float LdotN = dot(L, bumpNormal);
					float3 dif = fAtt * _PointLightColors[i].rgb * Kd * surfaceColor * saturate(LdotN);

					// Calculate specular reflections
					float Ks = _SpecularCoeff;
					float specN = _SpecularPower; // Values>>1 give tighter highlights
					float3 V = normalize(_WorldSpaceCameraPos - v.worldVertex.xyz);
					// Using Blinn-Phong approximation (note, this is a modification of normal Phong illumination):
					float3 H = normalize(V + L);
					float3 spe = fAtt * _PointLightColors[i].rgb * Ks * pow(saturate(dot(bumpNormal, H)), specN);

					dif_and_spe_sum += dif + spe;
				}

				// Combine Phong illumination model components
				float4 returnColor = float4(0.0f, 0.0f, 0.0f, 0.0f);
				returnColor.rgb = amb.rgb + dif_and_spe_sum.rgb;
				returnColor.a = surfaceColor.a;

				return returnColor;
			}
			ENDCG
		}
	}
}
