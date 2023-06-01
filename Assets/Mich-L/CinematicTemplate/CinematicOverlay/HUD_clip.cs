using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[ExecuteInEditMode]
public class HUD_clip : MonoBehaviour
{
    [Tooltip("Source timeline to retrieve current clip information.")]
    [SerializeField]
    public PlayableDirector m_masterPlayable;

    [Tooltip("Name of the track you want to retrieve the current clip from (eg: Cinemachine Track).")]
    [SerializeField]
    public string m_targetTrackName;

    [Tooltip("Text added before the name of the current clip playing in the targeted track (eg: title of your content).")]
    [SerializeField]
    public string m_prefix;

    [Tooltip("GameObject representing the root of a cinematic where children gameobjects starting with Sequence are sequences, and children of sequences starting with Shot are shots.")]
    [SerializeField]
    public GameObject m_cinematicRoot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var text = gameObject.GetComponent<Text>();

        if (text != null)
        {
            text.text = "";
            text.text += m_prefix;
            AddCurrentClipName(text);
            AddCinematicHierarchyNames(text);
        }
    }

    private void AddCinematicHierarchyNames(Text text)
    {
        if (m_cinematicRoot == null)
            return;

        foreach (Transform child in m_cinematicRoot.transform)
        {
            if (child.gameObject.activeSelf && child.gameObject.name.StartsWith("Sequence"))
            {
                text.text += child.gameObject.name + " - ";
                foreach (Transform grandChild in child)
                {
                    if (grandChild.gameObject.activeSelf && grandChild.gameObject.name.StartsWith("Shot"))
                    {
                        text.text += grandChild.gameObject.name;
                    }
                }
            }
        }
    }

    private void AddCurrentClipName(Text text)
    {
        if (m_masterPlayable == null)
            return;

        if (m_masterPlayable != null && m_masterPlayable.isActiveAndEnabled)
        {
            float s = (float)m_masterPlayable.time;

            TimelineAsset timeline = (TimelineAsset)m_masterPlayable.playableAsset;

            var tracks = timeline.GetOutputTracks();
            foreach (TrackAsset track in tracks)
            {
                if (track.name.Equals(m_targetTrackName))
                {
                    var clips = track.GetClips();
                    foreach (TimelineClip clip in clips)
                    {
                        float t0 = (float)clip.start;
                        float t1 = (float)clip.end;

                        if (t0 <= s && s < t1) text.text += clip.displayName + " ";
                    }
                    break;
                }
            }
        }
    }
}
