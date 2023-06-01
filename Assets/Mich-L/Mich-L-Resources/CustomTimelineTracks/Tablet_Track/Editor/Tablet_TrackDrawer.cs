using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

[CustomPropertyDrawer(typeof(Tablet_TrackBehaviour))]
public class Tablet_TrackDrawer : PropertyDrawer
{
    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        int fieldCount = 12;
        return fieldCount * EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty _IconsEmitAProp = property.FindPropertyRelative("_IconsEmitA");
        SerializedProperty _IconsOffsetAProp = property.FindPropertyRelative("_IconsOffsetA");
        SerializedProperty _WipeSwitchAProp = property.FindPropertyRelative("_WipeSwitchA");
        SerializedProperty _IconsEmitBProp = property.FindPropertyRelative("_IconsEmitB");
        SerializedProperty _IconsOffsetBProp = property.FindPropertyRelative("_IconsOffsetB");
        SerializedProperty _WipeSwitchBProp = property.FindPropertyRelative("_WipeSwitchB");
        SerializedProperty _IconsEmitCProp = property.FindPropertyRelative("_IconsEmitC");
        SerializedProperty _IconsOffsetCProp = property.FindPropertyRelative("_IconsOffsetC");
        SerializedProperty _WipeSwitchCProp = property.FindPropertyRelative("_WipeSwitchC");
        SerializedProperty _CursorAProp = property.FindPropertyRelative("_CursorA");
        SerializedProperty _CursorBProp = property.FindPropertyRelative("_CursorB");
        SerializedProperty _CursorCProp = property.FindPropertyRelative("_CursorC");

        Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(singleFieldRect, _IconsEmitAProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _IconsOffsetAProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _WipeSwitchAProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _IconsEmitBProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _IconsOffsetBProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _WipeSwitchBProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _IconsEmitCProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _IconsOffsetCProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _WipeSwitchCProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _CursorAProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _CursorBProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, _CursorCProp);
    }
}
