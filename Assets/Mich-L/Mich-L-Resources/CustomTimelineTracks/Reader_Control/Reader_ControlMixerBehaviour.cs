using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Reader_ControlMixerBehaviour : PlayableBehaviour
{
    float m_Default_ReaderEmitA;
    float m_Default_ReaderEmitB;
    float m_Default_ReaderEmitC;

    float m_Assigned_ReaderEmitA;
    float m_Assigned_ReaderEmitB;
    float m_Assigned_ReaderEmitC;

    TabletAndReaderSettings m_TrackBinding;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        m_TrackBinding = playerData as TabletAndReaderSettings;

        if (m_TrackBinding == null)
            return;

        if (!Mathf.Approximately(m_TrackBinding._ReaderEmitA, m_Assigned_ReaderEmitA))
            m_Default_ReaderEmitA = m_TrackBinding._ReaderEmitA;
        if (!Mathf.Approximately(m_TrackBinding._ReaderEmitB, m_Assigned_ReaderEmitB))
            m_Default_ReaderEmitB = m_TrackBinding._ReaderEmitB;
        if (!Mathf.Approximately(m_TrackBinding._ReaderEmitC, m_Assigned_ReaderEmitC))
            m_Default_ReaderEmitC = m_TrackBinding._ReaderEmitC;

        int inputCount = playable.GetInputCount ();

        float blended_ReaderEmitA = 0f;
        float blended_ReaderEmitB = 0f;
        float blended_ReaderEmitC = 0f;
        float totalWeight = 0f;
        float greatestWeight = 0f;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<Reader_ControlBehaviour> inputPlayable = (ScriptPlayable<Reader_ControlBehaviour>)playable.GetInput(i);
            Reader_ControlBehaviour input = inputPlayable.GetBehaviour ();
            
            blended_ReaderEmitA += input._ReaderEmitA * inputWeight;
            blended_ReaderEmitB += input._ReaderEmitB * inputWeight;
            blended_ReaderEmitC += input._ReaderEmitC * inputWeight;
            totalWeight += inputWeight;

            if (inputWeight > greatestWeight)
            {
                greatestWeight = inputWeight;
            }
        }

        m_Assigned_ReaderEmitA = blended_ReaderEmitA + m_Default_ReaderEmitA * (1f - totalWeight);
        m_TrackBinding._ReaderEmitA = m_Assigned_ReaderEmitA;
        m_Assigned_ReaderEmitB = blended_ReaderEmitB + m_Default_ReaderEmitB * (1f - totalWeight);
        m_TrackBinding._ReaderEmitB = m_Assigned_ReaderEmitB;
        m_Assigned_ReaderEmitC = blended_ReaderEmitC + m_Default_ReaderEmitC * (1f - totalWeight);
        m_TrackBinding._ReaderEmitC = m_Assigned_ReaderEmitC;

        m_TrackBinding.OnUpdateRenderSettings();
    }
}
