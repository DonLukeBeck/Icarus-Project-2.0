using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;
using System.ComponentModel;

[TrackColor(0.2947383f, 1f, 0.2783019f)]
[TrackClipType(typeof(Cyclo_BackgroundClip))]
[TrackBindingType(typeof(GlobalShaderCycloRenderSettings))]
[DisplayName("Cinematic Studio Sample/Cyclo Background Track")]
public class Cyclo_BackgroundTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<Cyclo_BackgroundMixerBehaviour>.Create (graph, inputCount);
    }

    // Please note this assumes only one component of type GlobalShaderCycloRenderSettings on the same gameobject.
    public override void GatherProperties (PlayableDirector director, IPropertyCollector driver)
    {
#if UNITY_EDITOR
        GlobalShaderCycloRenderSettings trackBinding = director.GetGenericBinding(this) as GlobalShaderCycloRenderSettings;
        if (trackBinding == null)
            return;

        // These field names are procedurally generated estimations based on the associated property names.
        // If any of the names are incorrect you will get a DrivenPropertyManager error saying it has failed to register the name.
        // In this case you will need to find the correct backing field name.
        // The suggested way of finding the field name is to:
        // 1. Make sure your scene is serialized to text.
        // 2. Search the text for the track binding component type.
        // 3. Look through the field names until you see one that looks correct.
        driver.AddFromName<GlobalShaderCycloRenderSettings>(trackBinding.gameObject, "_CycloBottomColor");
        driver.AddFromName<GlobalShaderCycloRenderSettings>(trackBinding.gameObject, "_CycloTopColor");
        driver.AddFromName<GlobalShaderCycloRenderSettings>(trackBinding.gameObject, "_CycloHorizonOrigin");
        driver.AddFromName<GlobalShaderCycloRenderSettings>(trackBinding.gameObject, "_CycloGradiantSpread");
#endif
        base.GatherProperties (director, driver);
    }
}
