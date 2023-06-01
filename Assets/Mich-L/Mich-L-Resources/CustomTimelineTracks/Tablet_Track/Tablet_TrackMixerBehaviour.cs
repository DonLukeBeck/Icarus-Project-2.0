using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Tablet_TrackMixerBehaviour : PlayableBehaviour
{
    float m_Default_IconsEmitA;
    float m_Default_IconsOffsetA;
    float m_Default_WipeSwitchA;
    float m_Default_IconsEmitB;
    float m_Default_IconsOffsetB;
    float m_Default_WipeSwitchB;
    float m_Default_IconsEmitC;
    float m_Default_IconsOffsetC;
    float m_Default_WipeSwitchC;
    int m_Default_CursorA;
    int m_Default_CursorB;
    int m_Default_CursorC;

    float m_Assigned_IconsEmitA;
    float m_Assigned_IconsOffsetA;
    float m_Assigned_WipeSwitchA;
    float m_Assigned_IconsEmitB;
    float m_Assigned_IconsOffsetB;
    float m_Assigned_WipeSwitchB;
    float m_Assigned_IconsEmitC;
    float m_Assigned_IconsOffsetC;
    float m_Assigned_WipeSwitchC;
    int m_Assigned_CursorA;
    int m_Assigned_CursorB;
    int m_Assigned_CursorC;

    TabletAndReaderSettings m_TrackBinding;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        m_TrackBinding = playerData as TabletAndReaderSettings;

        if (m_TrackBinding == null)
            return;

        if (!Mathf.Approximately(m_TrackBinding._IconsEmitA, m_Assigned_IconsEmitA))
            m_Default_IconsEmitA = m_TrackBinding._IconsEmitA;
        if (!Mathf.Approximately(m_TrackBinding._IconsOffsetA, m_Assigned_IconsOffsetA))
            m_Default_IconsOffsetA = m_TrackBinding._IconsOffsetA;
        if (!Mathf.Approximately(m_TrackBinding._WipeSwitchA, m_Assigned_WipeSwitchA))
            m_Default_WipeSwitchA = m_TrackBinding._WipeSwitchA;
        if (!Mathf.Approximately(m_TrackBinding._IconsEmitB, m_Assigned_IconsEmitB))
            m_Default_IconsEmitB = m_TrackBinding._IconsEmitB;
        if (!Mathf.Approximately(m_TrackBinding._IconsOffsetB, m_Assigned_IconsOffsetB))
            m_Default_IconsOffsetB = m_TrackBinding._IconsOffsetB;
        if (!Mathf.Approximately(m_TrackBinding._WipeSwitchB, m_Assigned_WipeSwitchB))
            m_Default_WipeSwitchB = m_TrackBinding._WipeSwitchB;
        if (!Mathf.Approximately(m_TrackBinding._IconsEmitC, m_Assigned_IconsEmitC))
            m_Default_IconsEmitC = m_TrackBinding._IconsEmitC;
        if (!Mathf.Approximately(m_TrackBinding._IconsOffsetC, m_Assigned_IconsOffsetC))
            m_Default_IconsOffsetC = m_TrackBinding._IconsOffsetC;
        if (!Mathf.Approximately(m_TrackBinding._WipeSwitchC, m_Assigned_WipeSwitchC))
            m_Default_WipeSwitchC = m_TrackBinding._WipeSwitchC;
        if (m_TrackBinding._CursorA != m_Assigned_CursorA)
            m_Default_CursorA = m_TrackBinding._CursorA;
        if (m_TrackBinding._CursorB != m_Assigned_CursorB)
            m_Default_CursorB = m_TrackBinding._CursorB;
        if (m_TrackBinding._CursorC != m_Assigned_CursorC)
            m_Default_CursorC = m_TrackBinding._CursorC;

        int inputCount = playable.GetInputCount ();

        float blended_IconsEmitA = 0f;
        float blended_IconsOffsetA = 0f;
        float blended_WipeSwitchA = 0f;
        float blended_IconsEmitB = 0f;
        float blended_IconsOffsetB = 0f;
        float blended_WipeSwitchB = 0f;
        float blended_IconsEmitC = 0f;
        float blended_IconsOffsetC = 0f;
        float blended_WipeSwitchC = 0f;
        float blended_CursorA = 0f;
        float blended_CursorB = 0f;
        float blended_CursorC = 0f;
        float totalWeight = 0f;
        float greatestWeight = 0f;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<Tablet_TrackBehaviour> inputPlayable = (ScriptPlayable<Tablet_TrackBehaviour>)playable.GetInput(i);
            Tablet_TrackBehaviour input = inputPlayable.GetBehaviour ();
            
            blended_IconsEmitA += input._IconsEmitA * inputWeight;
            blended_IconsOffsetA += input._IconsOffsetA * inputWeight;
            blended_WipeSwitchA += input._WipeSwitchA * inputWeight;
            blended_IconsEmitB += input._IconsEmitB * inputWeight;
            blended_IconsOffsetB += input._IconsOffsetB * inputWeight;
            blended_WipeSwitchB += input._WipeSwitchB * inputWeight;
            blended_IconsEmitC += input._IconsEmitC * inputWeight;
            blended_IconsOffsetC += input._IconsOffsetC * inputWeight;
            blended_WipeSwitchC += input._WipeSwitchC * inputWeight;
            blended_CursorA += input._CursorA * inputWeight;
            blended_CursorB += input._CursorB * inputWeight;
            blended_CursorC += input._CursorC * inputWeight;
            totalWeight += inputWeight;

            if (inputWeight > greatestWeight)
            {
                greatestWeight = inputWeight;
            }
        }

        m_Assigned_IconsEmitA = blended_IconsEmitA + m_Default_IconsEmitA * (1f - totalWeight);
        m_TrackBinding._IconsEmitA = m_Assigned_IconsEmitA;
        m_Assigned_IconsOffsetA = blended_IconsOffsetA + m_Default_IconsOffsetA * (1f - totalWeight);
        m_TrackBinding._IconsOffsetA = m_Assigned_IconsOffsetA;
        m_Assigned_WipeSwitchA = blended_WipeSwitchA + m_Default_WipeSwitchA * (1f - totalWeight);
        m_TrackBinding._WipeSwitchA = m_Assigned_WipeSwitchA;
        m_Assigned_IconsEmitB = blended_IconsEmitB + m_Default_IconsEmitB * (1f - totalWeight);
        m_TrackBinding._IconsEmitB = m_Assigned_IconsEmitB;
        m_Assigned_IconsOffsetB = blended_IconsOffsetB + m_Default_IconsOffsetB * (1f - totalWeight);
        m_TrackBinding._IconsOffsetB = m_Assigned_IconsOffsetB;
        m_Assigned_WipeSwitchB = blended_WipeSwitchB + m_Default_WipeSwitchB * (1f - totalWeight);
        m_TrackBinding._WipeSwitchB = m_Assigned_WipeSwitchB;
        m_Assigned_IconsEmitC = blended_IconsEmitC + m_Default_IconsEmitC * (1f - totalWeight);
        m_TrackBinding._IconsEmitC = m_Assigned_IconsEmitC;
        m_Assigned_IconsOffsetC = blended_IconsOffsetC + m_Default_IconsOffsetC * (1f - totalWeight);
        m_TrackBinding._IconsOffsetC = m_Assigned_IconsOffsetC;
        m_Assigned_WipeSwitchC = blended_WipeSwitchC + m_Default_WipeSwitchC * (1f - totalWeight);
        m_TrackBinding._WipeSwitchC = m_Assigned_WipeSwitchC;
        m_Assigned_CursorA = Mathf.RoundToInt (blended_CursorA + m_Default_CursorA * (1f - totalWeight));
        m_TrackBinding._CursorA = m_Assigned_CursorA;
        m_Assigned_CursorB = Mathf.RoundToInt (blended_CursorB + m_Default_CursorB * (1f - totalWeight));
        m_TrackBinding._CursorB = m_Assigned_CursorB;
        m_Assigned_CursorC = Mathf.RoundToInt (blended_CursorC + m_Default_CursorC * (1f - totalWeight));
        m_TrackBinding._CursorC = m_Assigned_CursorC;

        m_TrackBinding.OnUpdateRenderSettings();
    }
}
