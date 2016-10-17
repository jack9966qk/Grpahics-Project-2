


Shader "Unlit/FogShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Pass
		{
			ZWrite Off
		    Blend SrcAlpha OneMinusSrcAlpha // use alpha blending

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag


			#include "UnityCG.cginc"

			uniform float4 _FogColor;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;

			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 worldVertex : TEXCOORD1;

			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				float4 worldVertex = mul(unity_ObjectToWorld, v.vertex);
				o.worldVertex = worldVertex;
				return o;
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
				Ceye.a = 0.5;
				return Ceye;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				if(col.r<=0.5){
				    col = fixed4(0.0,0.0,0,1);
				}else if(col.r > 0.5){
					col = fixed4(0.2,0.2,1,1);
				}
				col = applyFog(col,i.worldVertex.z);

				return col;
			}


			ENDCG
		}
	}
}
