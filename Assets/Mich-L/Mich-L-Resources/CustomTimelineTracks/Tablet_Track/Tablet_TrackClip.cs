using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class Tablet_TrackClip : PlayableAsset, ITimelineClipAsset
{
    public Tablet_TrackBehaviour template = new Tablet_TrackBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<Tablet_TrackBehaviour>.Create (graph, template);
        return playable;
    }
}
