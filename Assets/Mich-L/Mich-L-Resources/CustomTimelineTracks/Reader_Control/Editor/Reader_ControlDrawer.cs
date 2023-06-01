using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

[CustomPropertyDrawer(typeof(Reader_ControlBehaviour))]
public class Reader_ControlDrawer : PropertyDrawer
{
    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        int fieldCount = 3;
        return fieldCount * EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty _ReaderEmitAProp = property.FindPropertyRelative("_ReaderEmitA");
        SerializedProperty _ReaderEmitBProp = property.FindPropertyRelative("_ReaderEmitB");
        SerializedProperty _ReaderEmitCProp = property.FindPropertyRelative("_ReaderEmitC");

        Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(singleFieldRect, _ReaderEmitAProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _ReaderEmitBProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _ReaderEmitCProp);
    }
}
