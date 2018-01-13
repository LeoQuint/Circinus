// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/PixelShader" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _PixelsX ("Pixels X", int) = 512
		_PixelsY ("Pixels Y", int) = 512
    }
    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows


        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
        };

        fixed4 _Color;
        fixed _PixelsX;
		fixed _PixelsY;

        void surf (Input IN, inout SurfaceOutputStandard o) {

            float dx = 1.0 / _PixelsX;
            float dy = 1.0 / _PixelsY;
            fixed2 Coord = fixed2(dx * floor(IN.uv_MainTex.x / dx), dy * floor(IN.uv_MainTex.y / dy)); 
            fixed4 c = tex2D(_MainTex, Coord);    

            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}