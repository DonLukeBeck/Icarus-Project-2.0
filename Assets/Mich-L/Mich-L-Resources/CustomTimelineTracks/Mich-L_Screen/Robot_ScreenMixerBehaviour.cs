using UnityEngine;
using UnityEngine.Playables;

public class Robot_ScreenMixerBehaviour : PlayableBehaviour
{
    float m_Default_TVNoise;
    int m_Default_ImageSequence;
    Color m_Default_ScreenEmissionColor;

    float m_Assigned_TVNoise;
    int m_Assigned_ImageSequence;
    Color m_Assigned_ScreenEmissionColor;

    GlobalShaderRenderSettings m_TrackBinding;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        m_TrackBinding = playerData as GlobalShaderRenderSettings;

        if (m_TrackBinding == null)
            return;

        if (!Mathf.Approximately(m_TrackBinding._TVNoise, m_Assigned_TVNoise))
            m_Default_TVNoise = m_TrackBinding._TVNoise;
        if (m_TrackBinding._ImageSequence != m_Assigned_ImageSequence)
            m_Default_ImageSequence = m_TrackBinding._ImageSequence;
        if (m_TrackBinding._ScreenEmissionColor != m_Assigned_ScreenEmissionColor)
            m_Default_ScreenEmissionColor = m_TrackBinding._ScreenEmissionColor;

        int inputCount = playable.GetInputCount ();

        float blended_TVNoise = 0f;
        float blended_ImageSequence = 0f;
        Color blended_ScreenEmissionColor = Color.clear;
        float totalWeight = 0f;
        float greatestWeight = 0f;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<Robot_ScreenBehaviour> inputPlayable = (ScriptPlayable<Robot_ScreenBehaviour>)playable.GetInput(i);
            Robot_ScreenBehaviour input = inputPlayable.GetBehaviour ();
            
            blended_TVNoise += input._TVNoise * inputWeight;
            blended_ImageSequence += input._ImageSequence * inputWeight;
            blended_ScreenEmissionColor += input._ScreenEmissionColor * inputWeight;
            totalWeight += inputWeight;

            if (inputWeight > greatestWeight)
            {
                greatestWeight = inputWeight;
            }
        }

        m_Assigned_TVNoise = blended_TVNoise + m_Default_TVNoise * (1f - totalWeight);
        m_TrackBinding._TVNoise = m_Assigned_TVNoise;
        m_Assigned_ImageSequence = Mathf.RoundToInt (blended_ImageSequence + m_Default_ImageSequence * (1f - totalWeight));
        m_TrackBinding._ImageSequence = m_Assigned_ImageSequence;
        m_Assigned_ScreenEmissionColor = blended_ScreenEmissionColor + m_Default_ScreenEmissionColor * (1f - totalWeight);
        m_TrackBinding._ScreenEmissionColor = m_Assigned_ScreenEmissionColor;

        m_TrackBinding.OnUpdateRenderSettings();
    }
}
