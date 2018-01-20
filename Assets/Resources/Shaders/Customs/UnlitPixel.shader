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
        
        #pragma surface surf NoLighting


        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
        };

        fixed4 _Color;
        fixed _PixelsX;
		fixed _PixelsY;

		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			 fixed4 c;
			 c.rgb = s.Albedo; 
			 c.a = s.Alpha;
			 return c;
		}

        void surf (Input IN, inout SurfaceOutput o) {

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