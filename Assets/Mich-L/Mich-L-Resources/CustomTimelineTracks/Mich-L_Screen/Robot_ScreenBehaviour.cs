using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class Robot_ScreenBehaviour : PlayableBehaviour
{
    public float _TVNoise;
    public int _ImageSequence;
    [ColorUsage(false, true)] public Color _ScreenEmissionColor;
}
