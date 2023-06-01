
// This file contains the vertex and fragment functions for the forward lit pass
// This is the shader pass that computes visible colors for a material
// by reading material, light, shadow, etc. data
// 
// Pull in URP library functions and our own common functions
#include "Packages/com.unity.render-pipelines.universal/Shaderlibrary/Lighting.hlsl"
#include "Packages/com.unity.render-pipelines.core/Shaderlibrary/ParallaxMapping.hlsl"
#include "MyLitCommon.hlsl"


// This attributes struct receives data about the mesh we're currently rendering
// Data is automatically placed in fields according to their semantic
struct Attributes {
	float3 positionOS : POSITION; // Position in object space
	float3 normalOS : NORMAL;
	float4 tangentOS : TANGENT;
	float2 uv : TEXCOORD0; // Material texture UVs
};

// This struct is output by the vertex function and input to the fragment function.
// Note that fields will be transformed by the intermediary rasterization stage
struct Interpolators {
	// This value should contain the position in clip space (which is similar to a position on screen)
	// when output from the vertex function. It will be transformed into pixel position of the current
	// fragment on the screen when read from the fragment function
	float4 positionCS : SV_POSITION;
	// The following variables will retain their values from the vertex stage, except the
	// rasterizer will interpolate them between vertices
	float2 uv : TEXCOORD0; // Material texture UVs
	float3 positionWS : TEXCOORD1;
	float3 normalWS : TEXCOORD2;
	float4 tangentWS : TEXCOORD3;
};

// The vertex function. This runs for each vertex on the mesh.
// It must output the position on the screen each vertex should appear at,
// as well as any data the fragment function will need
Interpolators Vertex(Attributes input) {
	Interpolators output;

	// These helper functions, found in URP/ShaderLib/ShaderVariablesFunctions.hlsl
	// transform object space values into world and clip space
	VertexPositionInputs posnInputs = GetVertexPositionInputs(input.positionOS);
	VertexNormalInputs normInputs = GetVertexNormalInputs(input.normalOS, input.tangentOS);

	// Pass position and orientation data to the fragment function
	output.positionCS = posnInputs.positionCS;
	//output.uv = TRANSFORM_TEX(input.uv, _Texture);
	output.uv = TRANSFORM_TEX(input.uv, _Texture);
	output.normalWS = normInputs.normalWS;
	output.tangentWS = float4(normInputs.tangentWS, input.tangentOS.w);
	output.positionWS = posnInputs.positionWS;
	return output;
}



// The fragment function. This runs once per fragment, which you can think of as a pixel on the screen
// It must output the final color of this pixel
float4 Fragment(Interpolators input
#ifdef _DOUBLE_SIDED_NORMALS
	, FRONT_FACE_TYPE frontFace : FRONT_FACE_SEMANTIC
#endif
) : SV_TARGET{
	// Not needed debug tool such as Rendering Debugger make this available
	// float4 normalSample = SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, uv);
	// float3 normalWS = UnpackNormal(normalSample);
	// return float4((normalWS + 1) * 0.5, 1); (each value range between -1 and 1 so +1 divide by two normalise each componenent)



	float3 normalWS = input.normalWS;
#ifdef _DOUBLE_SIDED_NORMALS
	normalWS = normalWS * IS_FRONT_VFACE(frontFace, 1, -1);
#endif

	float3 positionWS = input.positionWS;
	float3 viewDirWS = GetWorldSpaceNormalizeViewDir(positionWS);
	float3 viewDirTS = GetViewDirectionTangentSpace(input.tangentWS, normalWS, viewDirWS);

	float2 uv = input.uv;
	uv += ParallaxMapping(TEXTURE2D_ARGS(_ParallaxMap, sampler_ParallaxMap), viewDirTS, _ParallaxStrength, uv);

	float4 colorSample = SAMPLE_TEXTURE2D(_Texture, sampler_Texture, uv);
	TestAlphaClip(colorSample);

#ifdef _NORMALMAP
	float3 normalTS = UnpackNormalScale(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, uv), _NormalStrength);
	float3x3 tangentToWorld = CreateTangentToWorld(normalWS, input.tangentWS.xyz, input.tangentWS.w);
	normalWS = normalize(TransformTangentToWorld(normalTS, tangentToWorld));
#else
	float3 normalTS = float3(0, 0, 1);
	normalWS = normalize(normalWS);
#endif

	InputData lightingInput = (InputData)0;
	lightingInput.positionWS = positionWS;
	lightingInput.normalWS = normalWS;
	lightingInput.viewDirectionWS = viewDirWS;
	lightingInput.shadowCoord = TransformWorldToShadowCoord(positionWS);
	lightingInput.positionCS = input.positionCS;
#ifdef _NORMALMAP
	lightingInput.tangentToWorld = tangentToWorld; // For rendering debugger
#endif

	SurfaceData surfaceInput = (SurfaceData)0;
	surfaceInput.albedo = colorSample.rgb * _Color.rgb;
	surfaceInput.alpha = colorSample.a * _Color.a;

#ifdef _SPECULAR_SETUP
	surfaceInput.specular = SAMPLE_TEXTURE2D(_SpecularMap, sampler_SpecularMap, uv).rgb * _SpecularTint;
	surfaceInput.metallic = 0;
#else
	surfaceInput.specular = 1;
	surfaceInput.metallic = SAMPLE_TEXTURE2D(_MetalMask, sampler_MetalMask, uv).r * _Metalness;
#endif

	surfaceInput.smoothness = SAMPLE_TEXTURE2D(_SmoothnessMask, sampler_SmoothnessMask, uv).r * _Smoothness;
	surfaceInput.emission = SAMPLE_TEXTURE2D(_EmissionMap, sampler_EmissionMap, uv).r * _EmissionTint;
	surfaceInput.clearCoatMask = SAMPLE_TEXTURE2D(_ClearCoatMask, sampler_ClearCoatMask, uv).r * _ClearCoatStrength;
	surfaceInput.clearCoatSmoothness = SAMPLE_TEXTURE2D(_ClearCoatSmoothnessMask, sampler_ClearCoatSmoothnessMask, uv).r * _ClearCoatSmoothness;

	surfaceInput.normalTS = normalTS;

	return UniversalFragmentPBR(lightingInput, surfaceInput);
	//return UniversalFragmentBlinnPhong(lightingInput, surfaceInput);
	//return float4(colorSample.rgb * _Color.rgb, _Color.a);
}