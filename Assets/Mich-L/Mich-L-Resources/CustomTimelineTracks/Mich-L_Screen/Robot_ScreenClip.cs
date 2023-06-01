using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class Robot_ScreenClip : PlayableAsset, ITimelineClipAsset
{
    public Robot_ScreenBehaviour template = new Robot_ScreenBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<Robot_ScreenBehaviour>.Create (graph, template);
        return playable;
    }
}
