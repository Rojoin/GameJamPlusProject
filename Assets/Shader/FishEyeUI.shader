Shader "Custom/FishEyeEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Strength ("Fish-Eye Strength", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Strength;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float2 FisheyeDistort(float2 uv, float strength)
            {
                // Center of the screen (normalized UV)
                float2 center = float2(0.5, 0.5);
                
                // Calculate distance from center
                float2 dist = uv - center;
                
                // Calculate length (distance from the center point)
                float len = length(dist);
                
                // Apply the fish-eye distortion based on strength
                float distortion = pow(len, 1.0 - strength);
                
                // Reapply distortion and return new UV
                return center + normalize(dist) * distortion;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Apply fish-eye distortion to UV coordinates
                float2 distortedUV = FisheyeDistort(i.uv, _Strength);

                // Sample the texture with the distorted UVs
                fixed4 col = tex2D(_MainTex, distortedUV);

                return col;
            }
            ENDCG
        }
    }
    FallBack "Transparent"
}
