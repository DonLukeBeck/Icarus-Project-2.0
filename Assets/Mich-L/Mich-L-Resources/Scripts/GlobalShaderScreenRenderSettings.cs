using UnityEngine;

[ExecuteInEditMode]
public class GlobalShaderScreenRenderSettings : MonoBehaviour
{
    // This script controls the screens shaders (Glitch, noise...). Simple Global variables are exposed in the Shaders and tweaked here.

    [Header("BACKGROUND SCREENS DISPLAY")]
    [Range(0, 1)]
    public int _GlitchSceneScreen = 0;
    [Range(0, 1)]
    public float _TVNoiseSceneScreen = 0;
    [Range(0, 1)]
    public float _EmissionSceneScreen = 0;
    [Range(0, 1)]
    public int _GlitchGameScreen = 0;
    [Range(0, 1)]
    public float _TVNoiseGameScreen = 0;
    [Range(0, 1)]
    public float _EmissionGameScreen = 0;

    public void OnUpdateRenderSettings()
    {
        SetRender();
    }

    private void Awake()
    {
        SetRender();
    }

    private void OnEnable()
    {
        SetRender();
    }

    private void OnValidate()
    {
        SetRender();
    }

    private void SetRender()
    {
        Shader.SetGlobalFloat("_GlitchSceneScreen", _GlitchSceneScreen);
        Shader.SetGlobalFloat("_TVNoiseSceneScreen", _TVNoiseSceneScreen);
        Shader.SetGlobalFloat("_EmissionSceneScreen", _EmissionSceneScreen);
        Shader.SetGlobalFloat("_GlitchGameScreen", _GlitchGameScreen);
        Shader.SetGlobalFloat("_EmissionGameScreen", _EmissionGameScreen);
        Shader.SetGlobalFloat("_TVNoiseGameScreen", _TVNoiseGameScreen);
    }
}
