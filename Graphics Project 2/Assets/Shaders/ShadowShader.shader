Shader "Unlit/ShadowShader" {
    SubShader {

        Pass {
         
            Tags { "LightMode" = "ForwardBase" } 

            CGPROGRAM
 
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
			#include "UnityCG.cginc"
            #include "AutoLight.cginc"
 
			uniform sampler2D _MainTex;
			uniform sampler2D _NormalMapTex;


			struct vertIn {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				LIGHTING_COORDS(1, 2)

			};


			v2f vert(vertIn v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				TRANSFER_VERTEX_TO_FRAGMENT(o);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : COLOR{
				fixed4 o = tex2D(_MainTex, i.uv);
				float attenuation = LIGHT_ATTENUATION(i);
				return  o * attenuation;
			}

            ENDCG
        }
    }
    Fallback "VertexLit"
}