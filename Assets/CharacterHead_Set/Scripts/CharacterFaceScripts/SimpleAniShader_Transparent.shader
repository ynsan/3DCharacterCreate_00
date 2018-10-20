// 参考URL
// http://nn-hokuson.hatenablog.com/entry/2017/03/03/202309
// https://kido0617.github.io/unity/2015-01-03-shader-hue-shift/
// https://qiita.com/enpel/items/e88e8d97490ec618c630
// https://www.cg-method.com/entry/2017/09/12/100000/
// by PronamaChan Shader


Shader "SimpleAnime_Shader/EyeTransparent" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_PupilTex ("Decal Color", Color) = (1,1,1,1)

		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5

		[PerRendererData]
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_EyeTex ("Decal (RGBA)", 2D) = "black" {}
		_HighLightTex ("HighLight (RGBA)", 2D) = "black" {}
		_SpecialTex ("Special (RGBA)", 2D) = "black" {}

		_ScrollX("Scroll X", float) = 0
        _ScrollY("Scroll Y", float) = 0
	}

	SubShader {
	    Tags {
	    	"Queue" = "AlphaTest"
	    	"IgnoreProjector" = "True"
	    	"RenderType"="TransparentCutout"
	    }

	    LOD 250

	    Pass {
	    	Lighting Off
	    	Alphatest Greater [_Cutoff]
	    	SetTexture [_EyeTex] { combine texture } 
	    }
		CGPROGRAM
		#pragma surface surf Lambert alphatest:_Cutoff

		sampler2D _MainTex;
		sampler2D _EyeTex;
		sampler2D _HighLightTex;
		sampler2D _SpecialTex;

		fixed4 _Color;
		fixed4 _PupilTex;

		float _ScrollX, _ScrollY;

		struct Input {
		    float2 uv_MainTex;
		    float2 uv_EyeTex;
		    float2 uv_HighLightTex;
		    float2 uv_SpecialTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			// 

			half4 pupil = tex2D(_EyeTex, IN.uv_EyeTex) * _PupilTex;

			fixed4 c = tex2D(_EyeTex, IN.uv_EyeTex);
			float2 scroll = float2(_ScrollX, _ScrollY) * pupil.y;

		    half4 highlight = tex2D(_HighLightTex, IN.uv_HighLightTex);
		    half4 special = tex2D(_SpecialTex, IN.uv_SpecialTex);
		    c.rgb = lerp (c.rgb, pupil.rgb, pupil.a);
		    c.rgb = lerp (c.rgb, highlight.rgb, highlight.a);
		    c.rgb = lerp (c.rgb, special.a, special.rgb);
		    c *= _Color;

		    scroll = float2(_ScrollX, _ScrollY) * pupil.y;
		    pupil.y = tex2D(_EyeTex, pupil + scroll);
			pupil.y = highlight + scroll;
			pupil.y = special + scroll;

		    o.Albedo = c.rgb;
		    o.Alpha = c.a;
		}
	ENDCG
	}

	Fallback "Transparent/Cutout/VertexLit"
}
/*
Shader "SimpleAnime_Shader/EyeTransparent" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_PupilTex ("Decal Color", Color) = (1,1,1,1)
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5


		_MainTex ("Base (RGB)", 2D) = "white" {}
		_EyeTex ("Decal (RGBA)", 2D) = "black" {}
		_HighLightTex ("HighLight (RGBA)", 2D) = "black" {}
		_SpecialTex ("Special (RGBA)", 2D) = "black" {}

		_ScrollX("Scroll X", float) = 0
        _ScrollY("Scroll Y", float) = 0
	}

	SubShader {
	    Tags {
	    	"Queue" = "AlphaTest"
	    	"IgnoreProjector" = "True"
	    	"RenderType"="TransparentCutout"
	    }

	    LOD 250

	    Pass {
	    	Lighting Off
	    	Alphatest Greater [_Cutoff]
	    	SetTexture [_EyeTex] { combine texture } 
	    }
		CGPROGRAM
		#pragma surface surf Lambert alphatest:_Cutoff

		sampler2D _MainTex;
		sampler2D _EyeTex;
		sampler2D _HighLightTex;
		sampler2D _SpecialTex;

		fixed4 _Color;
		fixed4 _PupilTex;

		float _ScrollX, _ScrollY;

		struct Input {
		    float2 uv_MainTex;
		    float2 uv_EyeTex;
		    float2 uv_HighLightTex;
		    float2 uv_SpecialTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			// 
			fixed4 c = tex2D(_EyeTex, IN.uv_EyeTex);

			half4 pupil = tex2D(_MainTex, IN.uv_MainTex) * _PupilTex;
			float2 scroll = float2(_ScrollX, _ScrollY) * pupil.y;

		    half4 highlight = tex2D(_HighLightTex, IN.uv_HighLightTex);
		    half4 special = tex2D(_SpecialTex, IN.uv_SpecialTex);
		    c.rgb = lerp (c.rgb, pupil.rgb, pupil.a);
		    c.rgb = lerp (c.rgb, highlight.rgb, highlight.a);
		    c.rgb = lerp (c.rgb, special.a, special.rgb);
		    c *= _Color;

		    pupil = tex2D(_MainTex , IN.uv_MainTex + scroll);
		    c = tex2D(_EyeTex , pupil + scroll);
		    c = tex2D(_HighLightTex , highlight + scroll);
		    c = tex2D(_SpecialTex , special + scroll);

		    o.Albedo = c.rgb;
		    o.Alpha = c.a;
		}
	ENDCG
	}

	Fallback "Transparent/Cutout/VertexLit"
}
*/