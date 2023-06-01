using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(1f, 0.05694377f, 0f)]
[TrackClipType(typeof(Robot_ScreenClip))]
[TrackBindingType(typeof(GlobalShaderRenderSettings))]
[DisplayName("Cinematic Studio Sample/Robot Screen Track")]
public class Robot_ScreenTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<Robot_ScreenMixerBehaviour>.Create (graph, inputCount);
    }

    // Please note this assumes only one component of type GlobalShaderRenderSettings on the same gameobject.
    public override void GatherProperties (PlayableDirector director, IPropertyCollector driver)
    {
#if UNITY_EDITOR
        GlobalShaderRenderSettings trackBinding = director.GetGenericBinding(this) as GlobalShaderRenderSettings;
        if (trackBinding == null)
            return;

        // These field names are procedurally generated estimations based on the associated property names.
        // If any of the names are incorrect you will get a DrivenPropertyManager error saying it has failed to register the name.
        // In this case you will need to find the correct backing field name.
        // The suggested way of finding the field name is to:
        // 1. Make sure your scene is serialized to text.
        // 2. Search the text for the track binding component type.
        // 3. Look through the field names until you see one that looks correct.
        driver.AddFromName<GlobalShaderRenderSettings>(trackBinding.gameObject, "_TVNoise");
        driver.AddFromName<GlobalShaderRenderSettings>(trackBinding.gameObject, "_ImageSequence");
        driver.AddFromName<GlobalShaderRenderSettings>(trackBinding.gameObject, "_ScreenEmissionColor");
#endif
        base.GatherProperties (director, driver);
    }
}
