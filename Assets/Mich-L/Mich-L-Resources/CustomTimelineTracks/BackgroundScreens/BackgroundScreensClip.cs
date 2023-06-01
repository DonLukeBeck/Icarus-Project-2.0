using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class BackgroundScreensClip : PlayableAsset, ITimelineClipAsset
{
    public BackgroundScreensBehaviour template = new BackgroundScreensBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<BackgroundScreensBehaviour>.Create (graph, template);
        return playable;
    }
}
