
// This file contains the vertex and fragment functions for the forward lit pass
// This is the shader pass that computes visible colors for a material
// by reading material, light, shadow, etc. data
// 
// Pull in URP library functions and our own common functions
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
#include "brdf.hlsl"
#include "DisneyCommon.hlsl"

// This attributes struct receives data about the mesh we're currently rendering
// Data is automatically placed in fields according to their semantic
struct Attributes {
    float4 positionOS : POSITION; // Position in object space
    float2 uv : TEXCOORD0;  // Material texture UVs
    float3 normalOS : NORMAL; 
    float4 tangentOS : TANGENT;
};

// This struct is output by the vertex function and input to the fragment function.
// Note that fields will be transformed by the intermediary rasterization stage
// This struct is output by the vertex function and input to the fragment function.
// Note that fields will be transformed by the intermediary rasterization stage
struct Interpolators {
	float4 positionCS : SV_POSITION;
	// The following variables will retain their values from the vertex stage, except the
	// rasterizer will interpolate them between vertices
	float3 positionWS : TEXCOORD2;
	float3 normalWS : NORMAL;
	float4 tangentWS : TANGENT;

	float2 uv : TEXCOORD0;
	float3 world : TEXCOORD1;
};

Interpolators Vertex(Attributes input)
{
    Interpolators output;

	float3 world = mul(unity_ObjectToWorld, input.positionOS).xyz;

	VertexPositionInputs posnInputs = GetVertexPositionInputs(input.positionOS);
	VertexNormalInputs normInputs = GetVertexNormalInputs(input.normalOS, input.tangentOS);

	// Pass position and orientation data to the fragment function
	output.positionCS = posnInputs.positionCS;
	
	//output.uv = TRANSFORM_TEX(input.uv, _Texture);
	output.uv = input.uv;
	output.normalWS = normInputs.normalWS;
	output.tangentWS = float4(normInputs.tangentWS, input.tangentOS.w);
	output.positionWS = posnInputs.positionWS;
	output.world = world;
	return output;
}

float4 Fragment(Interpolators input) : SV_TARGET{
	float3 LightDirection = normalize(lerp(_MainLightPosition.xyz, _MainLightPosition.xyz - input.world, _MainLightPosition.w));
	float3 NormalDirection = normalize(mul((float3x3)unity_ObjectToWorld, input.normalWS));
	float3 ViewDirection = normalize(_WorldSpaceCameraPos.xyz - input.world);
	float3 WorldTangent = mul((float3x3)unity_ObjectToWorld, input.tangentWS.xyz);
	float3 WorldBinormal = cross(NormalDirection,WorldTangent) * input.tangentWS.w;
	
	VertexPositionInputs vertexInput = (VertexPositionInputs)0;
	vertexInput.positionWS = input.positionWS;
	float4 shadowCoord = GetShadowCoord(vertexInput);
	half shadowAttenutation = MainLightRealtimeShadow(shadowCoord);
	float4 color = lerp(half4(1, 1, 1, 1), _ShadowColor, (1.0 - shadowAttenutation) * _ShadowColor.a);

	return float4(BRDF(LightDirection, ViewDirection, NormalDirection, WorldTangent, WorldBinormal), 1.0) * float4(_Color,1.0) * color;
}