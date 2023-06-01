using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace Icarus
{
    [CustomEditor(typeof(ProceduralPlane))]
    public class ProceduralPlaneEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Generate"))
            {
                ProceduralPlane script = target as ProceduralPlane;
                script.Generate();
            }
        }
    }
}
#endif