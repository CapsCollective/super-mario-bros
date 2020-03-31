Shader "Hidden/CRT"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_ChannelStep ("Colour channel step size (pixels)", Int) = 1
		_Strength1 ("Strength of first disabled channel", Float) = 0.5
		_Strength2 ("Strength of second disabled channel", Float) = 0.5
		_Contrast ("Contrast", Float) = 1
		_Brightness ("Brightness", Float) = 1
		_ScanlineColour ("Scanline Colour", Color) = (0, 0, 0, 1)
		_ScanlineStep ("Scanline Step", Int) = 1
		_Curvature ("Screen Curvature", Float) = 1
    }

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float4 screenPos : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
				o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            sampler2D _MainTex;
			int _ChannelStep;
			float _Strength1;
			float _Strength2;
			float _Brightness;
			float _Contrast;
			float4 _ScanlineColour;
			int _ScanlineStep;
			float _Curvature;

            fixed4 frag (v2f i) : SV_Target
            {
				float2 pixelCoord = i.screenPos.xy * _ScreenParams.xy / i.screenPos.w;
				int pixelRound = (int)(pixelCoord.x / _ChannelStep) % 3;

				
				i.uv = i.uv - 0.5;
				i.uv *= 2;
				float2 uvSign = float2(sign(i.uv.x), sign(i.uv.y));
				i.uv = abs(i.uv);
				i.uv = (1 - i.uv);
				i.uv = pow(i.uv, _Curvature);
				i.uv = (1 - i.uv);
				i.uv *= uvSign;
				i.uv *= 0.5;
				i.uv += .5;

				/*
				i.screenPos.xy = i.screenPos.xy - 0.5;
				i.screenPos.xy *= 2;
				float2 uvSign = float2(sign(i.screenPos.xy.x), sign(i.screenPos.xy.y));
				i.screenPos.xy = abs(i.screenPos.xy);
				i.screenPos.xy = (1 - i.screenPos.xy);
				i.screenPos.xy = pow(i.screenPos.xy, _Curvature);
				i.screenPos.xy = (1 - i.screenPos.xy);
				i.screenPos.xy *= uvSign;
				i.screenPos.xy *= 0.5;
				i.screenPos.xy += .5;
				
				float2 pixelCoord = i.screenPos.xy * _ScreenParams.xy / i.screenPos.w;
				int pixelRound = (int)(pixelCoord.x / _ChannelStep) % 3;
				*/
				

				float4 outColour = float4(0, 0, 0, 1);
                float4 colour = tex2D(_MainTex, i.uv);

				float3 multiplier;
								
				if (pixelRound == 0)
				{
					multiplier = float3(1, _Strength1, _Strength2);
				}
				else if (pixelRound == 1) 
				{
					multiplier = float3(_Strength1, 1, _Strength2);
				}
				else
				{
					multiplier = float3(_Strength1, _Strength2, 1);
				}

				if ((int)(pixelCoord.y / _ScanlineStep) % 3 == 0) { multiplier *= _ScanlineColour; }
				
				outColour = colour * float4(multiplier, 1);
				outColour += _Brightness;
				outColour = pow(abs(outColour * 2 - 1), 1 / max(_Contrast, 0.0001)) * sign(outColour - 0.5) + 0.5;

				return lerp(
					outColour,
					0,
					lerp(
						pow(2 * max(abs(0.5 - i.uv.x), abs(0.5 - i.uv.y)), 5),
						2 * distance(
						i.uv,
						float2(0.5, 0.5)
						), .7
					) - .1
				);
            }
            ENDCG
        }
    }
}
