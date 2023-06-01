using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class Tablet_TrackBehaviour : PlayableBehaviour
{
    public float _IconsEmitA;
    public float _IconsOffsetA;
    public float _WipeSwitchA;
    public float _IconsEmitB;
    public float _IconsOffsetB;
    public float _WipeSwitchB;
    public float _IconsEmitC;
    public float _IconsOffsetC;
    public float _WipeSwitchC;
    public int _CursorA;
    public int _CursorB;
    public int _CursorC;
}
