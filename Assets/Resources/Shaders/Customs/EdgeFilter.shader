Shader "Custom/EdgeFilter" {

	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_DeltaX ("Delta X", Float) = 0.01
		_DeltaY ("Delta Y", Float) = 0.01
		_Threshold ("Threshold", Float) = 0.95
		_OutlineColor("OutlineColor", Color) = (1,1,1,1)
		_NearPixelBias("Near Pixel Bias", float) = 2
	}
	
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGINCLUDE
		
		#include "UnityCG.cginc"
		
		sampler2D _MainTex;
		float _DeltaX;
		float _DeltaY;
		float _Threshold;
		float4 _OutlineColor;
		float _NearPixelBias;

		float sobel (sampler2D tex, float2 uv) {
			float2 delta = float2(_DeltaX, _DeltaY);
			
			float4 hr = float4(0, 0, 0, 0);
			float4 vt = float4(0, 0, 0, 0);
			
			hr += tex2D(tex, (uv + float2(-1.0, -1.0) * delta)) *  1.0;
			hr += tex2D(tex, (uv + float2( 0.0, -1.0) * delta)) *  0.0;
			hr += tex2D(tex, (uv + float2( 1.0, -1.0) * delta)) * -1.0;
			hr += tex2D(tex, (uv + float2(-1.0,  0.0) * delta)) *  _NearPixelBias;
			hr += tex2D(tex, (uv + float2( 0.0,  0.0) * delta)) *  0.0;
			hr += tex2D(tex, (uv + float2( 1.0,  0.0) * delta)) * -_NearPixelBias;
			hr += tex2D(tex, (uv + float2(-1.0,  1.0) * delta)) *  1.0;
			hr += tex2D(tex, (uv + float2( 0.0,  1.0) * delta)) *  0.0;
			hr += tex2D(tex, (uv + float2( 1.0,  1.0) * delta)) * -1.0;
			
			vt += tex2D(tex, (uv + float2(-1.0, -1.0) * delta)) *  1.0;
			vt += tex2D(tex, (uv + float2( 0.0, -1.0) * delta)) *  _NearPixelBias;
			vt += tex2D(tex, (uv + float2( 1.0, -1.0) * delta)) *  1.0;
			vt += tex2D(tex, (uv + float2(-1.0,  0.0) * delta)) *  0.0;
			vt += tex2D(tex, (uv + float2( 0.0,  0.0) * delta)) *  0.0;
			vt += tex2D(tex, (uv + float2( 1.0,  0.0) * delta)) *  0.0;
			vt += tex2D(tex, (uv + float2(-1.0,  1.0) * delta)) * -1.0;
			vt += tex2D(tex, (uv + float2( 0.0,  1.0) * delta)) * -_NearPixelBias;
			vt += tex2D(tex, (uv + float2( 1.0,  1.0) * delta)) * -1.0;
			
			return sqrt(hr * hr + vt * vt);
		}
		
		float4 frag (v2f_img IN) : COLOR {
			
			fixed4 texcol = tex2D( _MainTex,IN.uv );
			float s = sobel(_MainTex, IN.uv);
			//ifs are bad, remove with max soon.
			if(s > _Threshold)
			{
				return _OutlineColor;
			}
			
			return texcol;
		}
		
		ENDCG
		
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			ENDCG
		}
		
	} 
	FallBack "Diffuse"
}