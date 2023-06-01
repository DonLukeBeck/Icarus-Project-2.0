using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

[CustomPropertyDrawer(typeof(Robot_ScreenBehaviour))]
public class Robot_ScreenDrawer : PropertyDrawer
{
    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        int fieldCount = 3;
        return fieldCount * EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty _TVNoiseProp = property.FindPropertyRelative("_TVNoise");
        SerializedProperty _ImageSequenceProp = property.FindPropertyRelative("_ImageSequence");
        SerializedProperty _ScreenEmissionColorProp = property.FindPropertyRelative("_ScreenEmissionColor");

        Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(singleFieldRect, _TVNoiseProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _ImageSequenceProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _ScreenEmissionColorProp);
    }
}
