// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Outline"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_Distance ("-", Float) = 1
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;
			float4 _MainTex_TexelSize;
			float _Distance;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				// Simple sobel filter for the alpha channel.
				float d = _MainTex_TexelSize.xy * _Distance;

				half a1 = SampleSpriteTexture(IN.texcoord + d * float2(-1, -1)).a;
		        half a2 = SampleSpriteTexture(IN.texcoord + d * float2( 0, -1)).a;
		        half a3 = SampleSpriteTexture(IN.texcoord + d * float2(+1, -1)).a;

		        half a4 = SampleSpriteTexture(IN.texcoord + d * float2(-1,  0)).a;
		        half a6 = SampleSpriteTexture(IN.texcoord + d * float2(+1,  0)).a;

		        half a7 = SampleSpriteTexture(IN.texcoord + d * float2(-1, +1)).a;
		        half a8 = SampleSpriteTexture(IN.texcoord + d * float2( 0, +1)).a;
		        half a9 = SampleSpriteTexture(IN.texcoord + d * float2(+1, +1)).a;

		        float gx = - a1 - a2*2 - a3 + a7 + a8*2 + a9;
		        float gy = - a1 - a4*2 - a7 + a3 + a6*2 + a9;

		        float w = sqrt(gx * gx + gy * gy) / 4;

		        // Mix the contour color.
		        half4 source = SampleSpriteTexture(IN.texcoord);
				half4 outlined = half4(lerp(source.rgb, _Color.rgb, w), 1);
				outlined.rgb *= outlined.a;
		        return half4(outlined.rgb, source.a);
			}
		ENDCG
		}
	}
}