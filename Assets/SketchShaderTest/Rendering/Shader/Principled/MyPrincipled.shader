//https://github.com/wdas/brdf/blob/main/src/brdfs/disney.brdf
//https://forum.unity.com/threads/disney-principled-brdf-shader.675697/
Shader "Disney"
{
    Properties
    {
        _ShadowColor("Shadow Color", Color) = (0.35,0.4,0.45,1.0)

        [Header(Surface options)]
        [MainTexture] _Texture("Texture", 2D) = "white" {}
        [MainColor] _Color("Color", Color) = (1,1,1,1)

        metallic("Metallic", Range(0.0,1.0)) = 0.0
        subsurface("Subsurface", Range(0.0,1.0)) = 0.0
        _specular("Specular", Range(0.0,1.0)) = 0.0
        roughness("Roughness", Range(0.0,1.0)) = 0.5
        specularTint("SpecularTint", Range(0.0,1.0)) = 0.0
        anisotropic("Anisotropic", Range(0.0,1.0)) = 0.0
        sheen("Sheen", Range(0.0,1.0)) = 0.0
        sheenTint("SheenTint", Range(0.0,1.0)) = 0.5
        clearcoat("Clearcoat", Range(0.0,1.0)) = 0.0
        clearcoatGloss("ClearcoatGloss", Range(0.0,1.0)) = 1.0

        [HideInInspector] _SourceBlend("Source blend", Float) = 0
        [HideInInspector] _DestBlend("Destiantion blend", Float) = 0
        [HideInInspector] _ZWrite("ZWrite", Float) = 0

        [HideInInspector] _SurfaceType("Surface type", Float) = 0
        [HideInInspector] _BlendType("Blend type", Float) = 0
        [HideInInspector] _FaceRenderingMode("Face rendering type", Float) = 0
    }
        SubShader
    {
        Pass
        {
            Name "ForwardPass" // For debugging
            Tags{"LightMode" = "UniversalForward"} // Pass specific tags. 
            HLSLPROGRAM

            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT
            
            #pragma vertex Vertex
            #pragma fragment Fragment

            // Include our code file
            #include "DisneyForwardLitPass.hlsl"

            ENDHLSL
        }

        Pass {
            Name "ShadowCaster" // For debugging
            Tags{"LightMode" = "ShadowCaster"} // Pass specific tags. 

            COLORMASK 0

            HLSLPROGRAM // Begin HLSL code

            // Register our programmable stage functions
            #pragma vertex Vertex
            #pragma fragment Fragment

            // Include our code file
            #include "DisneyShadowCasterPass.hlsl"
            ENDHLSL
        }
    }
}