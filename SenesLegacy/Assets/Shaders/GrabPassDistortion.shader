Shader "Custom/GrabPassDistortion"
{
	Properties
	{
		[HDR]
		_Color("Color", Color) = (1, 1, 1, 1)
		_Intensity("Intensity", Range(0, 15)) = 0
		_Amount("Amount", Range(0, 10)) = 0
	}

	SubShader
	{
		GrabPass{ "_GrabTexture" }

		Pass
		{
			Tags{ "Queue" = "Transparent" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f 
			{
				half4 pos : SV_POSITION;
				half4 grabPos : TEXCOORD0;
			};

			sampler2D _GrabTexture;
			half _Intensity;
			half _Amount;
			half4 _Color;

			v2f vert(appdata_base v) 
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.grabPos = ComputeGrabScreenPos(o.pos);
				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				i.grabPos.x += sin((_Time.y + i.grabPos.y * _Amount) * _Intensity) / 20;
			i.grabPos.y += -cos((_Time.y + i.grabPos.x * _Amount) * _Intensity) / 20;
				fixed4 color = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.grabPos));
				return color * _Color;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}