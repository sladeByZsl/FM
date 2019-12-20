Shader "S1Game/Scene/terrain_4map"
{
    Properties
    {
        _Color("Tint Color", color) = (1, 1, 1, 1)
        _Map_Control ("_Map_Control (RGBA)", 2D) = "black" {}
        _Map01 ("Map01", 2D) = "white" {}
        _Map02 ("Map02", 2D) = "white" {}
        _Map03 ("Map03", 2D) = "white" {}
        _Map04 ("Map04", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
        
            // #pragma multi_compile FOG_LINEAR
            // #pragma multi_compile FOG_OFF
            // #pragma multi_compile FOG_EXP
            // #pragma multi_compile FOG_EXP2
            // #pragma multi_compile_fwdbase
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 uv2:TEXCOORD1;
            };

            struct v2f
            {
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float2 uv3 : TEXCOORD3;
                float2 uv4 : TEXCOORD4;
                #ifdef LIGHTMAP_ON
                    float2 lightmap_uv:TEXCOORD5;
                #endif
                UNITY_FOG_COORDS(7)
                float4 vertex : SV_POSITION;
            };

            sampler2D _Map_Control,_Map01,_Map02,_Map03,_Map04;
            float4 _Color,_Map_Control_ST,_Map01_ST,_Map02_ST,_Map03_ST,_Map04_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv0= TRANSFORM_TEX(v.uv, _Map_Control);
                o.uv1= TRANSFORM_TEX(v.uv, _Map01);
                o.uv2= TRANSFORM_TEX(v.uv, _Map02);
                o.uv3= TRANSFORM_TEX(v.uv, _Map03);
                o.uv4= TRANSFORM_TEX(v.uv, _Map04);
                #ifdef LIGHTMAP_ON
                    o.lightmap_uv=v.uv2.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                #endif
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 splat = tex2D(_Map_Control, i.uv0);

                fixed4 col1= tex2D(_Map01, i.uv1);
                fixed4 col2= tex2D(_Map02, i.uv2);
                fixed4 col3= tex2D(_Map03, i.uv3);
                fixed4 col4= tex2D(_Map04, i.uv4);
                fixed4 final=col1*splat.r+col2*splat.g+col3*splat.b+col4*splat.a;
                #if defined(LIGHTMAP_ON)
                    half4 bakedColorTex = UNITY_SAMPLE_TEX2D(unity_Lightmap, i.lightmap_uv);
                    final.rgb*=DecodeLightmap(bakedColorTex);
                #endif
                final*=_Color;
                UNITY_APPLY_FOG(i.fogCoord, final);
                return final;
            }
            ENDCG
        }
    }
    fallback"Diffuse"
}
