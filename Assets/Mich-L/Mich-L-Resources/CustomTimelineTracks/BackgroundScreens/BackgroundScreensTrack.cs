using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;
using System.ComponentModel;

[TrackColor(1f, 0.7263846f, 0f)]
[TrackClipType(typeof(BackgroundScreensClip))]
[TrackBindingType(typeof(GlobalShaderScreenRenderSettings))]
[DisplayName("Cinematic Studio Sample/Background Screens Track")]
public class BackgroundScreensTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<BackgroundScreensMixerBehaviour>.Create (graph, inputCount);
    }

    // Please note this assumes only one component of type GlobalShaderRenderSettings on the same gameobject.
    public override void GatherProperties (PlayableDirector director, IPropertyCollector driver)
    {
#if UNITY_EDITOR
        GlobalShaderScreenRenderSettings trackBinding = director.GetGenericBinding(this) as GlobalShaderScreenRenderSettings;
        if (trackBinding == null)
            return;

        // These field names are procedurally generated estimations based on the associated property names.
        // If any of the names are incorrect you will get a DrivenPropertyManager error saying it has failed to register the name.
        // In this case you will need to find the correct backing field name.
        // The suggested way of finding the field name is to:
        // 1. Make sure your scene is serialized to text.
        // 2. Search the text for the track binding component type.
        // 3. Look through the field names until you see one that looks correct.
        driver.AddFromName<GlobalShaderScreenRenderSettings>(trackBinding.gameObject, "_GlitchSceneScreen");
        driver.AddFromName<GlobalShaderScreenRenderSettings>(trackBinding.gameObject, "_TVNoiseSceneScreen");
        driver.AddFromName<GlobalShaderScreenRenderSettings>(trackBinding.gameObject, "_EmissionSceneScreen");
        driver.AddFromName<GlobalShaderScreenRenderSettings>(trackBinding.gameObject, "_GlitchGameScreen");
        driver.AddFromName<GlobalShaderScreenRenderSettings>(trackBinding.gameObject, "_TVNoiseGameScreen");
        driver.AddFromName<GlobalShaderScreenRenderSettings>(trackBinding.gameObject, "_EmissionGameScreen");
#endif
        base.GatherProperties (director, driver);
    }
}
