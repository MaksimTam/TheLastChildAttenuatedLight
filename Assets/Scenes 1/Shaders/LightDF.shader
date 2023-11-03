
Shader "Custom/LightDF" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _EmissionStrength ("Emission Strength", Range(0, 1)) = 1
        _FlamePositionStart ("Flame Position Start", Range(0, 1)) = 0
        _FlamePositionEnd ("Flame Position End", Range(0, 1)) = 1
        _AnimationSpeed ("Animation Speed", Range(0, 1)) = 0.5
    }
    
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert alpha
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        fixed _EmissionStrength;
        fixed _FlamePositionStart;
        fixed _FlamePositionEnd;
        fixed _AnimationSpeed;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputAlpha o) {
            fixed4 texColor = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = texColor.rgb;
            o.Alpha = texColor.a;

            fixed flamePos = lerp(_FlamePositionStart, _FlamePositionEnd, sin(_AnimationSpeed * _Time.y));

            fixed4 emission = texColor * _EmissionStrength * smoothstep(0.9, 1, flamePos);
            o.Emission = emission.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
