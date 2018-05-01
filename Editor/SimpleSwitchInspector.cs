using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SimpleObjectTrigger))]
public class SimpleSwitchInspector : Editor
{


    public override void OnInspectorGUI()
    {
        var ss = target as SimpleObjectTrigger;

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