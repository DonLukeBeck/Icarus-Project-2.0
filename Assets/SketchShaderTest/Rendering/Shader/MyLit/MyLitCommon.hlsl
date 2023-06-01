#ifndef MY_LIT_COMMON_INCLUDED
#define MY_LIT_COMMON_INCLUDED

// Pull in URP library functions and our own common functions
#include "Packages/com.unity.render-pipelines.universal/Shaderlibrary/Lighting.hlsl"

// Textures
TEXTURE2D(_Texture); SAMPLER(sampler_Texture);// RGB = albedo, A = alpha
TEXTURE2D(_NormalMap); SAMPLER(sampler_NormalMap);// RGB = albedo, A = alpha
TEXTURE2D(_MetalMask); SAMPLER(sampler_MetalMask);// RGB = albedo, A = alpha
TEXTURE2D(_SpecularMap); SAMPLER(sampler_SpecularMap);// RGB = albedo, A = alpha
TEXTURE2D(_SmoothnessMask); SAMPLER(sampler_SmoothnessMask);// RGB = albedo, A = alpha
TEXTURE2D(_EmissionMap); SAMPLER(sampler_EmissionMap);// RGB = albedo, A = alpha
TEXTURE2D(_ParallaxMap); SAMPLER(sampler_ParallaxMap);// RGB = albedo, A = alpha
TEXTURE2D(_ClearCoatMask); SAMPLER(sampler_ClearCoatMask);
TEXTURE2D(_ClearCoatSmoothnessMask); SAMPLER(sampler_ClearCoatSmoothnessMask);

float4 _Texture_ST; // This is automatically set by Unity. Used in TRANSFORM_TEX to apply UV tiling
float4 _NormalMap_ST; // This is automatically set by Unity. Used in TRANSFORM_TEX to apply UV tiling

float4 _Color;
float _Smoothness;
float _Cutoff; 
float _NormalStrength; 
float _Metalness;  
float3 _SpecularTint; 
float3 _EmissionTint; 
float _ParallaxStrength;
float _ClearCoatStrength;
float _ClearCoatSmoothness;

void TestAlphaClip(float4 colorSample) {
#ifdef _ALPHA_CUTOUT
	clip(colorSample.a * _Color.a - _Cutoff);
#endif
}

#endif