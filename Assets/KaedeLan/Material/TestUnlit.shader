Shader "Unlit/hong_test"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _brightness("brightness", Range(0 , 3)) = 1
        _saturation("saturation", Range(0 , 2)) = 1
        _contrast("contrast", Range(0 , 2)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Fade" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _brightness;
            float _saturation;
            float _contrast;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed gray = 0.2125 * col.r + 0.7154 * col.g + 0.0721 * col.b;
                fixed3 grayColor = fixed3(gray, gray, gray);
                fixed3 finalColor = col * _brightness;
                finalColor = lerp(grayColor, finalColor, _saturation);
                fixed avgColor = fixed3(0.5, 0.5, 0.5);
                finalColor = lerp(avgColor, finalColor, _contrast);
                // apply fog
                col = fixed4(finalColor, col.a);
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}