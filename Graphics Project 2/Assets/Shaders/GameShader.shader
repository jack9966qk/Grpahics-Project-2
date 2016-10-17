Shader "Unlit/GameShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{

	    Tags { "Queue" = "Transparent" } 

		Pass
		{

			ZWrite Off
		    Blend SrcAlpha OneMinusSrcAlpha // use alpha blending

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma enable_d3d11_debug_symbols 

			uniform float _AmbientCoeff;
			uniform float _DiffuseCoeff;
			uniform float _SpecularCoeff;
			uniform float _SpecularPower;

			uniform int _applyTransparent;
			uniform float4 _FogColor;

			uniform sampler2D _MainTex;
			uniform sampler2D _NormalMapTex;

			uniform float4 _LightColor0;

			#include "UnityCG.cginc"
			#include "UnityDeferredLibrary.cginc"

			struct vertIn
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 normal : NORMAL;
				float4 tangent : TANGENT;
			};

			struct vertOut
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 worldVertex : TEXCOORD1;
				float3 worldNormal : TEXCOORD2;
				float3 worldTangent : TEXCOORD3;
				float3 worldBinormal : TEXCOORD4;
			};

			float4 _MainTex_ST;
			
			vertOut applyVertPhongBumpTex(vertIn v, vertOut o) {
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

			fixed4 sampleTexture(vertOut v){

				// Sample colour from texture (i.e. pixel colour before lighting applied)
				fixed4 surfaceColor = tex2D(_MainTex, v.uv);

				if (_applyTransparent == 1) {
					if (surfaceColor.r < 0.5){
						surfaceColor.a = 0;
					}else {
						surfaceColor.b = 1;
					}
				}

				return surfaceColor;
			}


			fixed4 applyFragPhongBumpTex(vertOut v, fixed4 surfaceColor) {
				// ===========MODIFIED HERE=========
				_AmbientCoeff = 1;
				_DiffuseCoeff = 1;
				_SpecularCoeff = 0.25;
				_SpecularPower = 5;


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

				//===================MODIFIED HERE==============
				float4 pointLightPosition = _WorldSpaceLightPos0;
				fixed4 pointLightColor = _LightColor0;

				/*for (int i = 0; i < _NumPointLights; i++)
				{*/
					// Calculate diffuse RBG reflections, we save the results of L.N because we will use it again
					// (when calculating the reflected ray in our specular component)
					float fAtt = 1;
					float Kd = _DiffuseCoeff;
//					float3 L = normalize(_WorldSpaceLightPos0.xyz - v.worldVertex.xyz);
					float3 L = normalize(-pointLightPosition.xyz);
					float LdotN = dot(L, bumpNormal);
					float3 dif = fAtt * pointLightColor.rgb * Kd * surfaceColor * saturate(LdotN);

					// Calculate specular reflections
					float Ks = _SpecularCoeff;
					float specN = _SpecularPower; // Values>>1 give tighter highlights
					float3 V = normalize(_WorldSpaceCameraPos - v.worldVertex.xyz);
					// Using Blinn-Phong approximation (note, this is a modification of normal Phong illumination):
					float3 H = normalize(V + L);
					float3 spe = fAtt * pointLightColor.rgb * Ks * pow(saturate(dot(bumpNormal, H)), specN);

					dif_and_spe_sum += dif + spe;
				//}

				// Combine Phong illumination model components
				float4 returnColor = float4(0.0f, 0.0f, 0.0f, 0.0f);
				returnColor.rgb = amb.rgb + dif_and_spe_sum.rgb;
				returnColor.a = surfaceColor.a;
				return returnColor;
			}
			
			fixed4 applyFog(fixed4 Cobject, float z) {
				
				float dist = abs(_WorldSpaceCameraPos.z - z);
				fixed4 Cfog = _FogColor;

				float f = -log2(dist / 15);

				if (f >= 1) {
					f = 1;
				}
				else if (f <= 0) {
					f = 0;
				}

				fixed4 Ceye = f * Cobject + (1 - f) * Cfog;
				return Ceye;
			}


			vertOut vert (vertIn v)
			{
				vertOut o;
				o = applyVertPhongBumpTex(v, o);
				return o;
			}


			fixed4 frag (vertOut v) : SV_Target
			{
				fixed4 o = sampleTexture(v);
				o = applyFragPhongBumpTex(v,o);
				o = applyFog(o,v.worldVertex.z);
				return o;
			}
			ENDCG
		}
	}
}
