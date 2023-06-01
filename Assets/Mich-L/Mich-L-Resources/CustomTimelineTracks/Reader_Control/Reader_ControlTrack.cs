using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;
using System.ComponentModel;

[TrackColor(0.240566f, 1f, 0.4467448f)]
[TrackClipType(typeof(Reader_ControlClip))]
[TrackBindingType(typeof(TabletAndReaderSettings))]
[DisplayName("Cinematic Studio Sample/Reader Control Track")]
public class Reader_ControlTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<Reader_ControlMixerBehaviour>.Create (graph, inputCount);
    }

    // Please note this assumes only one component of type TabletAndReaderSettings on the same gameobject.
    public override void GatherProperties (PlayableDirector director, IPropertyCollector driver)
    {
#if UNITY_EDITOR
        TabletAndReaderSettings trackBinding = director.GetGenericBinding(this) as TabletAndReaderSettings;
        if (trackBinding == null)
            return;

        // These field names are procedurally generated estimations based on the associated property names.
        // If any of the names are incorrect you will get a DrivenPropertyManager error saying it has failed to register the name.
        // In this case you will need to find the correct backing field name.
        // The suggested way of finding the field name is to:
        // 1. Make sure your scene is serialized to text.
        // 2. Search the text for the track binding component type.
        // 3. Look through the field names until you see one that looks correct.
        driver.AddFromName<TabletAndReaderSettings>(trackBinding.gameObject, "_ReaderEmitA");
        driver.AddFromName<TabletAndReaderSettings>(trackBinding.gameObject, "_ReaderEmitB");
        driver.AddFromName<TabletAndReaderSettings>(trackBinding.gameObject, "_ReaderEmitC");
#endif
        base.GatherProperties (director, driver);
    }
}
