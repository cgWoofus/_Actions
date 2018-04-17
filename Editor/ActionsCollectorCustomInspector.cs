using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(ActionsCollector))]
public class ActionsCollectorCustomInspector : Editor
{


    public override void OnInspectorGUI()
    {
        var ac = target as ActionsCollector;
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();


        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();            
            EditorUtility.SetDirty(ac);
        }


    }
}
