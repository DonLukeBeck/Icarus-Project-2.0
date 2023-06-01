using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[ExecuteInEditMode]
public class HUD_frame : MonoBehaviour
{
    [Tooltip("Source timeline to retrieve timing information.")]
    [SerializeField]
    public PlayableDirector m_masterPlayable;

    // Update is called once per frame
    void Update()
    {
        var text = gameObject.GetComponent<Text>();

        if (text != null && m_masterPlayable != null && m_masterPlayable.isActiveAndEnabled)
        {
            TimelineAsset timeline = (TimelineAsset)m_masterPlayable.playableAsset;
            Debug.Assert(timeline != null);

            var s   = (float)m_masterPlayable.time;
            var fps = (float)timeline.editorSettings.frameRate;
            int   f   = Mathf.RoundToInt(fps * s);
            float total_frames = Mathf.RoundToInt(fps * (float)m_masterPlayable.duration);

            string niceTime = string.Format("{0:00}:{1:00}.{2:00}",
                    Mathf.Floor(s / 60), //minutes
                    Mathf.Floor(s) % 60, //seconds
                    Mathf.Floor((s * 100) % 100)); //miliseconds

            text.text = niceTime + " [" + f + "//" + total_frames + "]";
        }
    }
}
