
#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

// This is a neat trick to work around a bug in the shader graph when
// enabling shadow keywords. Created by @cyanilux
// https://github.com/Cyanilux/URP_ShaderGraphCustomLighting
#ifndef SHADERGRAPH_PREVIEW
    #if (SHADERPASS != SHADERPASS_FORWARD)
    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #undef REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
    #endif
#endif

struct CustomLightingData {
    // Position and orientation
    float3 positionWS;
    float3 normalWS;
    float4 shadowCoord;
};



float CalculateCustomLighting(CustomLightingData d) {
#ifdef SHADERGRAPH_PREVIEW
    return 1; 
#else
    Light mainLight = GetMainLight(d.shadowCoord, d.positionWS, 1);
    float intensity = saturate(dot(d.normalWS, mainLight.direction));

    // Get the main light. Located in URP/ShaderLibrary/Lighting.hlsl

    return intensity * mainLight.shadowAttenuation;
#endif

}

void CalculateCustomLighting_float(float3 Position, float3 Normal, float3 ViewDirection, out float ShadowAttenuation) {

    CustomLightingData d;
    d.positionWS = Position;
    d.normalWS = Normal;

#ifdef SHADERGRAPH_PREVIEW
    // In preview, there's no shadows or bakedGI
    d.shadowCoord = 0;
#else
    // Calculate the main light shadow coord
    // There are two types depending on if cascades are enabled
    float4 positionCS = TransformWorldToHClip(Position);
#if SHADOWS_SCREEN
    d.shadowCoord = ComputeScreenPos(positionCS);
#else
    d.shadowCoord = TransformWorldToShadowCoord(Position);
#endif
#endif
    ShadowAttenuation = CalculateCustomLighting(d);
}
#endif