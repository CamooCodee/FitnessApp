// Adapted from explanation at https://www.reddit.com/r/Unity3D/comments/20jh1n/overlapping_opacity_shadow/

Shader "Custom/ShadowShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	_Colour("Colour", Color) = (0,0,0,1)
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "TransparentCutout"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
	}

		Cull Off
		Lighting Off
		ZWrite Off

		Pass // Set alpha to first shadow
	{
		ColorMask A
		Blend One Zero
		Stencil
	{
		Ref 3
		Comp NotEqual
		Pass Replace
	}

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		uniform sampler2D _MainTex;

	struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		half4 pos : SV_POSITION;
		half2 uv : TEXCOORD0;
	};

	float4 _MainTex_ST;

	v2f vert(appdata v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		return o;
	}

	half4 frag(v2f i) : SV_TARGET
	{
		fixed4 colour = tex2D(_MainTex, i.uv);
	return colour;
	}
		ENDCG
	}

		Pass // Find max shadow alpha and set pixel alpha to that
	{
		ColorMask A
		BlendOp Max

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		uniform sampler2D _MainTex;

	struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		half4 pos : SV_POSITION;
		half2 uv : TEXCOORD0;
	};

	float4 _MainTex_ST;

	v2f vert(appdata v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		return o;
	}

	half4 frag(v2f i) : SV_TARGET
	{
		fixed4 colour = tex2D(_MainTex, i.uv);
	return colour;
	}
		ENDCG

	}

		Pass // Apply colour to pixel in the amount of current alpha and background will be (1 - current alpha)
	{
		Stencil // Only needs to happen once
	{
		Ref 4
		Comp NotEqual
		Pass Replace
	}
		Blend DstAlpha OneMinusDstAlpha

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		uniform sampler2D _MainTex;

	struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		half4 pos : SV_POSITION;
	};

	v2f vert(appdata v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		return o;
	}

	half4 _Colour;

	half4 frag(v2f i) : SV_TARGET
	{

		return _Colour;
	}
		ENDCG

	}
	}

		Fallback off
}