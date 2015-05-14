Shader "CTD/CustomCel"
{

	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)
		_SpecColor("Specular Color", Color) = (1,1,1,1)
		_Shininess("Spec Power", Range(0, 2)) = 0.5
		_MainTex("Base RGB Gloss (A)", 2D) = "white"{}
		_BumpMap("Normal Map", 2D) = "white"{}
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimPower("Rim Power", Range(0.5, 8.0)) = 3.0
		
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 300
		
		CGPROGRAM
		#pragma surface surf BlinnPhong
		
		float4 _Color;
		sampler2D _MainTex;
		sampler2D _BumpMap;
		float4 _RimColor;
		float _RimPower;
		float _Shininess;
		
		struct Input
		{		
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 viewDir;			
		};
		
		void surf (Input IN, inout SurfaceOutput o)
		{
			
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			float3 bump = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			
			o.Normal = bump.rgb;
			
			o.Albedo = tex.rgb * _Color.rgb;
			
			o.Specular = _Shininess;
			o.Gloss = tex.a;
			
			
			
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
			o.Emission = _RimColor.rgb * pow(rim, _RimPower);
			
		
		}
		
		ENDCG
	}
	
	Fallback "Diffuse"


}
