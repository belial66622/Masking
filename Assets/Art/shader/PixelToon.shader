Shader "Custom/PixelToon"
{
    Properties
    {
        _BaseMap ("Base Map", 2D) = "white" {}
        _ShadowColor ("Shadow Color", Color) = (0.5, 0.5, 0.5, 1)
        _ShadowThreshold ("Shadow Threshold", Range(0, 1)) = 0.5
        _Cutoff ("Alpha Cutoff", Range(0.0, 1.0)) = 0.5
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="TransparentCutout" 
            "RenderPipeline"="UniversalPipeline" 
            "Queue"="AlphaTest"
        }
        LOD 100

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma multi_compile _ _SHADOWS_SOFT

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float2 uv : TEXCOORD2;
            };

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseMap_ST;
                float4 _ShadowColor;
                float _ShadowThreshold;
                float _Cutoff;
            CBUFFER_END

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            Varyings vert(Attributes input)
            {
                Varyings output;
                
                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                output.positionCS = vertexInput.positionCS;
                output.positionWS = vertexInput.positionWS;
                
                VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, float4(0,0,0,0));
                output.normalWS = normalInput.normalWS;
                
                output.uv = TRANSFORM_TEX(input.uv, _BaseMap);
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                // Sample Base Texture
                half4 baseColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv);

                // Alpha Cutoff (Transparency)
                clip(baseColor.a - _Cutoff);

                // Initialize shadow color (tinted)
                half4 shadowColorResult = baseColor * _ShadowColor;

                // 1. Get Main Light
                #if defined(UNIVERSAL_LIGHTING_INCLUDED)
                    Light mainLight = GetMainLight(TransformWorldToShadowCoord(input.positionWS));
                #else
                    Light mainLight = GetMainLight(); 
                #endif
                
                // 2. Normal Dot Light (NdotL)
                float3 normal = normalize(input.normalWS);
                float3 lightDir = normalize(mainLight.direction);
                float NdotL = max(0.0, dot(normal, lightDir));

                // 3. Hard-Edge Shadows
                float lightStep = step(_ShadowThreshold, NdotL);
                
                // Incorporate cast shadows
                lightStep *= mainLight.shadowAttenuation; 
                float isLit = step(0.01, lightStep); 

                // 4. Color Selection
                half3 finalRGB = lerp(shadowColorResult.rgb, baseColor.rgb, isLit);

                return half4(finalRGB, baseColor.a);
            }
            ENDHLSL
        }
        
        // ShadowCaster Pass
        Pass
        {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }

            ZWrite On
            ZTest LEqual
            ColorMask 0

            HLSLPROGRAM
            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseMap_ST;
                float4 _ShadowColor;
                float _ShadowThreshold;
                float _Cutoff;
            CBUFFER_END
            
            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);
            float3 _LightDirection;

            Varyings ShadowPassVertex(Attributes input)
            {
                Varyings output;
                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                output.positionCS = GetShadowCoord(vertexInput);
                output.uv = TRANSFORM_TEX(input.uv, _BaseMap);
                return output;
            }

            half4 ShadowPassFragment(Varyings input) : SV_Target
            {
                half4 baseColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv);
                clip(baseColor.a - _Cutoff);
                return 0;
            }
            ENDHLSL
        }
    }
}
