using UnityEngine;


[ExecuteInEditMode]
public class TabletAndReaderSettings : MonoBehaviour
{
    // This script controls the Tablet Parameters. Simple Global variables are exposed in the Shaders and tweaked here.

    [Header("SCREENS PARAMETERS")]
    public Color _BaseColor = Color.black;
    public Color _FrameColor = Color.grey;
    public Color _IconsColor = Color.white;
    [ColorUsage(false, true)] public Color _EmitColor = Color.white;
    [Header("ICONS CONTROLLERS")]
    [Header("_A")]
    [Range(0, 1)]
    public float _WipeSwitchA = 0;
    [Range(0, 1)]
    public float _IconsOffsetA = 0;
    [Range(0, 1)]
    public float _IconsEmitA = 0;
    [Header("_B")]
    [Range(0, 1)]
    public float _WipeSwitchB = 0;
    [Range(0, 1)]
    public float _IconsOffsetB = 0;
    [Range(0, 1)]
    public float _IconsEmitB = 0;
    [Header("_C")]
    [Range(0, 1)]
    public float _WipeSwitchC = 0;
    [Range(0, 1)]
    public float _IconsOffsetC = 0;
    [Range(0, 1)]
    public float _IconsEmitC = 0;
    [Header("CURSORS CONTROLLERS")]
    [Range(0, 1)]
    public int _CursorA = 0;
    [Range(0, 1)]
    public int _CursorB = 0;
    [Range(0, 1)]
    public int _CursorC = 0;
    [Header("READER CONTROLLERS")]
    [Range(0, 1)]
    public float _ReaderEmitA = 0;	
    [Range(0, 1)]
    public float _ReaderEmitB = 0;
    [Range(0, 1)]
    public float _ReaderEmitC = 0;

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
        Shader.SetGlobalColor("_BaseColor", _BaseColor);
        Shader.SetGlobalColor("_FrameColor", _FrameColor);
        Shader.SetGlobalColor("_IconsColor", _IconsColor);
        Shader.SetGlobalColor("_EmitColor", _EmitColor);
        Shader.SetGlobalFloat("_WipeSwitchA", _WipeSwitchA);
        Shader.SetGlobalFloat("_IconsOffsetA", _IconsOffsetA);
        Shader.SetGlobalFloat("_IconsEmitA", _IconsEmitA);
        Shader.SetGlobalFloat("_WipeSwitchB", _WipeSwitchB);
        Shader.SetGlobalFloat("_IconsOffsetB", _IconsOffsetB);
        Shader.SetGlobalFloat("_IconsEmitB", _IconsEmitB);
        Shader.SetGlobalFloat("_WipeSwitchC", _WipeSwitchC);
        Shader.SetGlobalFloat("_IconsOffsetC", _IconsOffsetC);
        Shader.SetGlobalFloat("_IconsEmitC", _IconsEmitC);
        Shader.SetGlobalFloat("_CursorA", _CursorA);
        Shader.SetGlobalFloat("_CursorB", _CursorB);
        Shader.SetGlobalFloat("_CursorC", _CursorC);
        Shader.SetGlobalFloat("_ReaderEmitA", _ReaderEmitA);
        Shader.SetGlobalFloat("_ReaderEmitB", _ReaderEmitB);
        Shader.SetGlobalFloat("_ReaderEmitC", _ReaderEmitC);
    }
}
