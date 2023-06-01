using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class Cyclo_BackgroundClip : PlayableAsset, ITimelineClipAsset
{
    public Cyclo_BackgroundBehaviour template = new Cyclo_BackgroundBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<Cyclo_BackgroundBehaviour>.Create (graph, template);
        return playable;
    }
}
