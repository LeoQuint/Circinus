Shader "Custom/ColorBand"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BandsLeft ("BandsLeft", Range(0,150)) = 150
		_BandsRight ("BandsRight", Range(0,150)) = 150
		_LinePosition ("LinePosition", Range(0,1)) = 1
		_LineSize ("LineSize", Range(0,0.05)) = 0.01
		_LineColor ("LineColor", Color) = (1,1,1,1)
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
			fixed _BandsLeft;
			fixed _BandsRight;
			float _LinePosition;
			fixed _LineSize;
			fixed4 _LineColor;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed ColorPass(fixed val, fixed band)
			{
				return round(val * band)/band;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				if(i.uv.x <= (_LinePosition + _LineSize) && i.uv.x >= (_LinePosition - _LineSize))
				{
					col = _LineColor;
				}
				else if(i.uv.x < _LinePosition)
				{
					col.r = ColorPass(col.r, _BandsLeft);
					col.g = ColorPass(col.g, _BandsLeft);
					col.b = ColorPass(col.b, _BandsLeft);
				}
				else
				{
					col.r = ColorPass(col.r, _BandsRight);
					col.g = ColorPass(col.g, _BandsRight);
					col.b = ColorPass(col.b, _BandsRight);
				}
				
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
