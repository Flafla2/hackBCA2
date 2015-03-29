Shader "Custom/StencilWriter" {
	Properties {
		_Layer ("Stencil Layer", Float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		
		ZWrite Off
		Stencil {
			Ref [_Layer]
			Comp Always
			Pass Replace
		}
		Colormask 0
		
		Pass {}
	} 
	FallBack Off
}
