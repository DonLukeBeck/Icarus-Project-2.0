using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

[CustomPropertyDrawer(typeof(BackgroundScreensBehaviour))]
public class BackgroundScreensDrawer : PropertyDrawer
{
    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        int fieldCount = 6;
        return fieldCount * EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty _GlitchSceneScreenProp = property.FindPropertyRelative("_GlitchSceneScreen");
        SerializedProperty _TVNoiseSceneScreenProp = property.FindPropertyRelative("_TVNoiseSceneScreen");
        SerializedProperty _EmissionSceneScreenProp = property.FindPropertyRelative("_EmissionSceneScreen");
        SerializedProperty _GlitchGameScreenProp = property.FindPropertyRelative("_GlitchGameScreen");
        SerializedProperty _TVNoiseGameScreenProp = property.FindPropertyRelative("_TVNoiseGameScreen");
        SerializedProperty _EmissionGameScreenProp = property.FindPropertyRelative("_EmissionGameScreen");

        Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(singleFieldRect, _GlitchSceneScreenProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _TVNoiseSceneScreenProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _EmissionSceneScreenProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _GlitchGameScreenProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _TVNoiseGameScreenProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _EmissionGameScreenProp);
    }
}
