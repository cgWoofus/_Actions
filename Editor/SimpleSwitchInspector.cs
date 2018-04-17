using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SimpleSwitch))]
public class SimpleSwitchInspector : Editor
{


    public override void OnInspectorGUI()
    {
        var ss = target as SimpleSwitch;

        EditorGUI.BeginChangeCheck();

        base.OnInspectorGUI();
  //      base.DrawDefaultInspector();

        if (GUILayout.Button("press"))
            {
                ss.GO();                    
            }

        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(ss);

    }
}