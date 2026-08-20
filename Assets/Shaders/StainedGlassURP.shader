Shader "Custom/StainedGlassURP"
{
    Properties
    {
        [Header(Glass Pane Colors - edit these freely)]
        _Color1 ("Pane Color 1", Color) = (0.85, 0.1, 0.15, 1)
        _Color2 ("Pane Color 2", Color) = (0.1, 0.35, 0.75, 1)
        _Color3 ("Pane Color 3", Color) = (0.95, 0.75, 0.1, 1)
        _Color4 ("Pane Color 4", Color) = (0.15, 0.6, 0.3, 1)
        _Color5 ("Pane Color 5", Color) = (0.6, 0.2, 0.7, 1)

        [Header(Pane Pattern)]
        _CellDensity ("Cell Density", Range(1, 40)) = 8
        _CellRandomness ("Cell Irregularity", Range(0, 1)) = 0.85
        _PatternSeed ("Pattern Seed", Range(0, 100)) = 0
        _ColorVariation ("Color Variation Within Pane", Range(0, 0.5)) = 0.08

        [Header(Lead Came Lines)]
        _LeadColor ("Lead Line Color", Color) = (0.02, 0.02, 0.02, 1)
        _LeadWidth ("Lead Line Width", Range(0.001, 0.15)) = 0.035
        _LeadShine ("Lead Line Metallic Shine", Range(0, 1)) = 0.6

        [Header(Glass Look)]
        _Glossiness ("Glass Smoothness", Range(0, 1)) = 0.85
        _FresnelPower ("Fresnel Rim Power", Range(0.1, 8)) = 3
        _RimColor ("Fresnel Rim Color", Color) = (1, 1, 1, 1)
        _RimIntensity ("Fresnel Rim Intensity", Range(0, 2)) = 0.5

        [Header(Light Transmission)]
        _Translucency ("Backlight Transmission", Range(0, 3)) = 1.2
        _TransPower ("Transmission Sharpness", Range(1, 16)) = 4
        _GlassAlpha ("Overall Opacity", Range(0, 1)) = 0.75

        [Header(Ambient Fill)]
        _AmbientStrength ("Ambient Fill Strength", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }
        LOD 200

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            Cull Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
                float3 normalWS    : TEXCOORD1;
                float3 viewDirWS   : TEXCOORD2;
            };

            // SRP Batcher compatible material property block
            CBUFFER_START(UnityPerMaterial)
                float4 _Color1, _Color2, _Color3, _Color4, _Color5;
                float _CellDensity, _CellRandomness, _PatternSeed, _ColorVariation;
                float4 _LeadColor;
                float _LeadWidth, _LeadShine;
                float _Glossiness, _FresnelPower, _RimIntensity;
                float4 _RimColor;
                float _Translucency, _TransPower, _GlassAlpha;
                float _AmbientStrength;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                VertexPositionInputs positions = GetVertexPositionInputs(IN.positionOS.xyz);
                OUT.positionHCS = positions.positionCS;
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.viewDirWS = GetWorldSpaceNormalizeViewDir(positions.positionWS);
                OUT.uv = IN.uv;
                return OUT;
            }

            // ---------- hash / Voronoi helpers ----------
            float2 hash2(float2 p)
            {
                p = float2(
                    dot(p, float2(127.1, 311.7)) + _PatternSeed * 17.0,
                    dot(p, float2(269.5, 183.3)) + _PatternSeed * 31.0
                );
                return frac(sin(p) * 43758.5453123);
            }

            // x = distance to nearest cell center (F1)
            // y = distance to second nearest cell center (F2)
            // zw = integer id of the nearest cell, used to pick a pane color
            float4 voronoi(float2 uv)
            {
                float2 g = floor(uv);
                float2 f = frac(uv);

                float f1 = 8.0;
                float f2 = 8.0;
                float2 id1 = 0;

                for (int y = -1; y <= 1; y++)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        float2 neighbor = float2(x, y);
                        float2 pointPos = neighbor + hash2(g + neighbor) * _CellRandomness;
                        float dist = length(pointPos - f);

                        if (dist < f1)
                        {
                            f2 = f1;
                            f1 = dist;
                            id1 = g + neighbor;
                        }
                        else if (dist < f2)
                        {
                            f2 = dist;
                        }
                    }
                }
                return float4(f1, f2, id1);
            }

            float3 paletteColor(float2 cellId)
            {
                float h = hash2(cellId).x;
                float3 col;
                if (h < 0.2)      col = _Color1.rgb;
                else if (h < 0.4) col = _Color2.rgb;
                else if (h < 0.6) col = _Color3.rgb;
                else if (h < 0.8) col = _Color4.rgb;
                else              col = _Color5.rgb;

                // subtle per-cell brightness variation so panes don't look flat
                float variation = (hash2(cellId + 5.17).y - 0.5) * _ColorVariation;
                return saturate(col + variation);
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float3 normalWS = normalize(IN.normalWS);
                float3 viewDirWS = normalize(IN.viewDirWS);

                float2 uv = IN.uv * _CellDensity;
                float4 v = voronoi(uv);
                float edgeDist = v.y - v.x; // gap between the two nearest cell borders

                float3 glassColor = paletteColor(v.zw);
                float leadMask = 1 - smoothstep(0.0, _LeadWidth, edgeDist);

                float3 albedo = lerp(glassColor, _LeadColor.rgb, leadMask);
                float specularAmount = lerp(_Glossiness, _LeadShine, leadMask);
                float alpha = lerp(_GlassAlpha, 1, leadMask); // lead lines opaque, panes translucent

                Light mainLight = GetMainLight();
                float3 lightDir = normalize(mainLight.direction);
                float3 lightColor = mainLight.color * mainLight.distanceAttenuation;

                float ndotl = max(0, dot(normalWS, lightDir));

                // Fake transmission: brighten panes when the light sits roughly behind
                // the surface relative to the viewer, mimicking light shining through glass.
                float backLight = pow(saturate(dot(viewDirWS, -lightDir)), _TransPower);
                float transmission = backLight * _Translucency * (1 - leadMask);

                float3 h = normalize(lightDir + viewDirWS);
                float nh = max(0, dot(normalWS, h));
                float spec = pow(nh, lerp(16, 128, specularAmount));

                // Fresnel rim highlight for a shiny glass edge
                float fresnel = pow(1 - saturate(dot(viewDirWS, normalWS)), _FresnelPower) * _RimIntensity;

                // Ambient fill from baked light probes / environment, so the shadow side isn't pure black
                float3 ambient = SampleSH(normalWS) * albedo * _AmbientStrength;

                float3 color = albedo * lightColor * (ndotl * 0.6 + 0.4);
                color += albedo * lightColor * transmission;
                color += spec * lightColor;
                color += _RimColor.rgb * fresnel;
                color += ambient;

                return half4(color, alpha);
            }
            ENDHLSL
        }
    }

    FallBack Off
}
