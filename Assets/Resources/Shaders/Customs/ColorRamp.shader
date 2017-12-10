Shader "Custom/ColorRamp"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RampTex ("Ramp texture", 2D) = "white" {}
        _Index ("Index", Range(0, 1)) = 1
		_Brightness ("Brightness", Range(0,5)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
 
            #include "UnityCG.cginc"
           
            sampler2D _MainTex;
            sampler2D _RampTex;
            float _Index;
			float _Brightness;

            fixed4 frag (v2f_img i) : SV_Target
            {
                fixed4 mainTex = tex2D(_MainTex, i.uv);
				fixed4 rampTex =  tex2D(_RampTex, fixed2(_Index, 0.5));	
				rampTex.a = 0.75;

				fixed4 mainTexVisible = mainTex.rgba * (1-rampTex.a);
				fixed4 overlayTexVisible = rampTex.rgba * (rampTex.a);
             
				fixed4 finalColor = (mainTexVisible + overlayTexVisible) * _Brightness;

                return finalColor;		
            }
            ENDCG
        }
    }
}