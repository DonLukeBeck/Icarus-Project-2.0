Shader "Icarus/MyLit" {

    Properties{
        [Header(Surface options)]
        [MainTexture] _Texture("Texture", 2D) = "white" {}
        [MainColor] _Color("Color", Color) = (1,1,1,1)
        _Cutoff("Alpha cutout threshold", Range(0,1)) =0.5
        [NoScaleOffset][Normal]_NormalMap("Normal", 2D) = "bump" {}
        _NormalStrength("Normal Strength", Range(0,1)) = 1
        [NoScaleOffset]_MetalMask("Metalness mask", 2D) = "white" {}
        _Metalness("Metalness", Range(0,1)) = 0
        [Toggle(_SPECULAR_SETUP)] _SpecularSetupToggle("Use specular workflow", Float) = 0
        [NoScaleOffset]_SpecularMap("Specular map", 2D) = "white" {}
        _SpecularTint("Specular tint", Color) = (1,1,1,1)
        [NoScaleOffset]_SmoothnessMask("Smoothness mask", 2D) = "white" {}
        _Smoothness("Smoothness", Range(0,1)) = 0
        [NoScaleOffset]_EmissionMap("Emission map", 2D) = "white" {}
        [HDR]_EmissionTint("Emission tint", Color) = (0,0,0,0)
        [NoScaleOffset]_HeightMap("Height map", 2D) = "white" {}
        _ParallaxStrength("Parallax Strength", Range(0,1)) = 0.05
        [NoScaleOffset] _ClearCoatMask("Clear coat mask", 2D) = "white" {}
        _ClearCoatStrength("Clear coat strength", Range(0, 1)) = 0
        [NoScaleOffset] _ClearCoatSmoothnessMask("Clear coat smoothness mask", 2D) = "white" {}
        _ClearCoatSmoothness("Clear coat smoothness", Range(0, 1)) = 0

        [HideInInspector] _Cull("Cull mode", Float) = 2
        [HideInInspector] _SourceBlend("Source blend", Float) = 0
        [HideInInspector] _DestBlend("Destiantion blend", Float) = 0
        [HideInInspector] _ZWrite("ZWrite", Float) = 0
        
        [HideInInspector] _SurfaceType("Surface type", Float) = 0
        [HideInInspector] _BlendType("Blend type", Float) = 0
        [HideInInspector] _FaceRenderingMode("Face rendering type", Float) = 0
    }
    // Subshaders allow for different behaviour and options for different pipelines and platforms
    SubShader{
        // These tags are shared by all passes in this sub shader
        Tags{"RenderPipeline" = "UniversalPipeline" "RenderType" = "Opaque"}

        // Shaders can have several passes which are used to render different data about the material
        // Each pass has it's own vertex and fragment function and shader variant keywords
        Pass {
            Name "ForwardLit" // For debugging
            Tags{"LightMode" = "UniversalForward"} // Pass specific tags. 
        // "UniversalForward" tells Unity this is the main lighting pass of this shader

            Blend[_SourceBlend][_DestBlend]
            ZWrite[_ZWrite]
            Cull[_Cull]

            HLSLPROGRAM // Begin HLSL code

            #define _CLEARCOATMAP
            #pragma shader_feature_local_fragment _NORMALMAP

            #pragma shader_feature_local _ALPHA_CUTOUT
            #pragma shader_feature_local _DOUBLE_SIDED_NORMALS
            #pragma shader_feature_local_fragment _SPECULAR_SETUP

            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile_fragment _ _SHADOWS_SOFT

            #pragma multi_compile_fragment _ DEBUG_DISPLAY

            // Register our programmable stage functions
            #pragma vertex Vertex
            #pragma fragment Fragment

            // Include our code file
            #include "MyLitForwardLitPass.hlsl"
            ENDHLSL
        }
        Pass {
            Name "ShadowCaster" // For debugging
            Tags{"LightMode" = "ShadowCaster"} // Pass specific tags. 

            COLORMASK 0
            Cull[_Cull]

            HLSLPROGRAM // Begin HLSL code

            #pragma shader_feature_local _ALPHA_CUTOUT
            #pragma shader_feature_local _DOUBLE_SIDED_NORMALS

            // Register our programmable stage functions
            #pragma vertex Vertex
            #pragma fragment Fragment

            // Include our code file
            #include "MyLitShadowCasterPass.hlsl"
            ENDHLSL
        }
    }

    CustomEditor "MyLitCustomInspector"
}