Shader "BLINK/World Object" {
	Properties {
		_Color ("Color (Primary/Far)", Color) = (1,1,1,1)
		_Albedo ("Albedo (Primary/Far)", 2D) = "white" {}

		_Smoothness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		[HideInInspector] _BorderColor ("Border Color", Color) = (1,1,1,1)
		[HideInInspector] _BorderWidth ("Border Width", Float) = 0.1
		[HideInInspector] _LightPos ("Texturer Position", Vector) = (0,0,0,0)
		[HideInInspector] _LightRad ("Texturer Radius", Float) = 1.0
		[HideInInspector] _Cylindrical ("Cylindrical (Ignore Y axis)", Float) = 0
		
		[HideInInspector] _TexturizerState ("Texturizer State", Float) = 0 // 0 = full, 1 = radial -- used to support both types of switching
		[HideInInspector] _Inverted ("Invert State", Float) = 0 // 0 = normal, 1 = inverted -- used to invert the world via scripting
	}
	SubShader {
		Tags { "RenderType"="TransparentCutout" "Queue"="AlphaTest" }
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _Albedo;
		half4 _Color;

		float _Smoothness;
		float _Metallic;
		
		half4 _BorderColor;
		float _BorderWidth;

		float4 _LightPos;
		float _LightRad;

		half _Cylindrical;
		half _TexturizerState;
		half _Inverted;

		struct Input {
			float2 uv_AlbedoPrimary;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float3 d = (float3)_LightPos-IN.worldPos;
			// Using Distance Squared eliminates the need for a costly square root calculation
			float distSq = d.x*d.x + d.z*d.z + (1.0f-_Cylindrical)*d.y*d.y;
			float farDistSq = _LightRad+_BorderWidth/2; farDistSq *= farDistSq;
			float nearDistSq = _LightRad-_BorderWidth/2; nearDistSq *= nearDistSq;
             
			// Alpha (gradient between inner and outer texture) is calculated using the distance
			// saturate() is used to limit the alpha to the range 0 - 1
			float alpha = saturate((distSq-nearDistSq)/(farDistSq-nearDistSq));

			// Border alpha is the alpha of the border (0-1) at a point.  This is cast to an integer so there is a hard border.
			// Remove (int) to get a soft gradient border.
			float border_alpha = (int)abs(alpha*2-1) * _TexturizerState;

			if(!_TexturizerState) {alpha = 1;border_alpha = 1;} // Ugly if statements in a shader
			if(_Inverted) alpha = 1-alpha;
			
			if(alpha == 0) discard;

			half4 c = tex2D (_Albedo, IN.uv_AlbedoPrimary) * _Color;

			o.Albedo = lerp(fixed3(0,0,0),c,border_alpha);
			//o.Alpha = lerp(c_n.a,c_f.a,alpha);
			o.Smoothness = _Smoothness;
			o.Metallic = _Metallic;
             
			o.Emission = lerp((fixed3)_BorderColor,fixed3(0,0,0),border_alpha);
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
