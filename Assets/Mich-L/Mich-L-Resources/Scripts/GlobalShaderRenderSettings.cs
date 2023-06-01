using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class GlobalShaderRenderSettings : MonoBehaviour
{
    // This script controls the Character Shader Variables (Color, face noise...). Simple Global variables are exposed in the Shaders and tweaked here.


    [Header("MICH-L COLORS (Vertex Color RGBBW for ABCDE)")]
    [Space(20)]
    [FormerlySerializedAs("_MichelLivery")]
    public RobotLivery _RobotLivery;
    //[Space(20)]
    [Header("MICH-L SCREEN")]
    [Range(0, 1)]
    public float _TVNoise = 0;
    [Range(0, 1)]
    public int _ImageSequence = 0;
    [ColorUsage(false, true)] public Color _ScreenEmissionColor;
    //[Space(20)]
    [Header("MICH-L STICKER")]
    public Texture2D _Sticker;

    public void OnUpdateRenderSettings()
    {
        SetRender();
    }

    private void Start()
    {
        SetRender();
    }

    private void OnValidate()
    {
        SetRender();
    }

    private void SetRender()
    {
        Shader.SetGlobalColor("_Mich_lColorA", _RobotLivery._RobotColorA);
        Shader.SetGlobalColor("_Mich_lColorB", _RobotLivery._RobotColorB);
        Shader.SetGlobalColor("_Mich_lColorC", _RobotLivery._RobotColorC);
        Shader.SetGlobalColor("_Mich_lColorD", _RobotLivery._RobotColorD);
        Shader.SetGlobalColor("_Mich_lColorE", _RobotLivery._RobotColorE);
        Shader.SetGlobalFloat("_TVNoise", _TVNoise);
        Shader.SetGlobalFloat("_ImageSequence", _ImageSequence);
        Shader.SetGlobalColor("_ScreenEmissionColor", _ScreenEmissionColor);
        Shader.SetGlobalTexture("_Sticker", _Sticker);
    }
}
