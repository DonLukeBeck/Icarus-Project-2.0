using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class BackgroundScreensMixerBehaviour : PlayableBehaviour
{
    int m_Default_GlitchSceneScreen;
    float m_Default_TVNoiseSceneScreen;
    float m_Default_EmissionSceneScreen;
    int m_Default_GlitchGameScreen;
    float m_Default_TVNoiseGameScreen;
    float m_Default_EmissionGameScreen;

    int m_Assigned_GlitchSceneScreen;
    float m_Assigned_TVNoiseSceneScreen;
    float m_Assigned_EmissionSceneScreen;
    int m_Assigned_GlitchGameScreen;
    float m_Assigned_TVNoiseGameScreen;
    float m_Assigned_EmissionGameScreen;

    GlobalShaderScreenRenderSettings m_TrackBinding;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        m_TrackBinding = playerData as GlobalShaderScreenRenderSettings;

        if (m_TrackBinding == null)
            return;

        if (m_TrackBinding._GlitchSceneScreen != m_Assigned_GlitchSceneScreen)
            m_Default_GlitchSceneScreen = m_TrackBinding._GlitchSceneScreen;
        if (!Mathf.Approximately(m_TrackBinding._TVNoiseSceneScreen, m_Assigned_TVNoiseSceneScreen))
            m_Default_TVNoiseSceneScreen = m_TrackBinding._TVNoiseSceneScreen;
        if (!Mathf.Approximately(m_TrackBinding._EmissionSceneScreen, m_Assigned_EmissionSceneScreen))
            m_Default_EmissionSceneScreen = m_TrackBinding._EmissionSceneScreen;
        if (m_TrackBinding._GlitchGameScreen != m_Assigned_GlitchGameScreen)
            m_Default_GlitchGameScreen = m_TrackBinding._GlitchGameScreen;
        if (!Mathf.Approximately(m_TrackBinding._TVNoiseGameScreen, m_Assigned_TVNoiseGameScreen))
            m_Default_TVNoiseGameScreen = m_TrackBinding._TVNoiseGameScreen;
        if (!Mathf.Approximately(m_TrackBinding._EmissionGameScreen, m_Assigned_EmissionGameScreen))
            m_Default_EmissionGameScreen = m_TrackBinding._EmissionGameScreen;

        int inputCount = playable.GetInputCount ();

        float blended_GlitchSceneScreen = 0f;
        float blended_TVNoiseSceneScreen = 0f;
        float blended_EmissionSceneScreen = 0f;
        float blended_GlitchGameScreen = 0f;
        float blended_TVNoiseGameScreen = 0f;
        float blended_EmissionGameScreen = 0f;
        float totalWeight = 0f;
        float greatestWeight = 0f;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<BackgroundScreensBehaviour> inputPlayable = (ScriptPlayable<BackgroundScreensBehaviour>)playable.GetInput(i);
            BackgroundScreensBehaviour input = inputPlayable.GetBehaviour ();
            
            blended_GlitchSceneScreen += input._GlitchSceneScreen * inputWeight;
            blended_TVNoiseSceneScreen += input._TVNoiseSceneScreen * inputWeight;
            blended_EmissionSceneScreen += input._EmissionSceneScreen * inputWeight;
            blended_GlitchGameScreen += input._GlitchGameScreen * inputWeight;
            blended_TVNoiseGameScreen += input._TVNoiseGameScreen * inputWeight;
            blended_EmissionGameScreen += input._EmissionGameScreen * inputWeight;
            totalWeight += inputWeight;

            if (inputWeight > greatestWeight)
            {
                greatestWeight = inputWeight;
            }
        }

        m_Assigned_GlitchSceneScreen = Mathf.RoundToInt (blended_GlitchSceneScreen + m_Default_GlitchSceneScreen * (1f - totalWeight));
        m_TrackBinding._GlitchSceneScreen = m_Assigned_GlitchSceneScreen;
        m_Assigned_TVNoiseSceneScreen = blended_TVNoiseSceneScreen + m_Default_TVNoiseSceneScreen * (1f - totalWeight);
        m_TrackBinding._TVNoiseSceneScreen = m_Assigned_TVNoiseSceneScreen;
        m_Assigned_EmissionSceneScreen = blended_EmissionSceneScreen + m_Default_EmissionSceneScreen * (1f - totalWeight);
        m_TrackBinding._EmissionSceneScreen = m_Assigned_EmissionSceneScreen;
        m_Assigned_GlitchGameScreen = Mathf.RoundToInt (blended_GlitchGameScreen + m_Default_GlitchGameScreen * (1f - totalWeight));
        m_TrackBinding._GlitchGameScreen = m_Assigned_GlitchGameScreen;
        m_Assigned_TVNoiseGameScreen = blended_TVNoiseGameScreen + m_Default_TVNoiseGameScreen * (1f - totalWeight);
        m_TrackBinding._TVNoiseGameScreen = m_Assigned_TVNoiseGameScreen;
        m_Assigned_EmissionGameScreen = blended_EmissionGameScreen + m_Default_EmissionGameScreen * (1f - totalWeight);
        m_TrackBinding._EmissionGameScreen = m_Assigned_EmissionGameScreen;

        m_TrackBinding.OnUpdateRenderSettings();
    }
}
