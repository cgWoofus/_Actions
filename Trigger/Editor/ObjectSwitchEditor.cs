using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectSwitch))]
 class ObjectSwitchEditor : Editor
    {
    bool value = true;
    public override void OnInspectorGUI()
    {
        var ac = target as ObjectSwitch;
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();

        if( GUILayout.Button("go"))
        {
            var nv = !value;
            value = nv;
            ac.Go(nv);
        }
        

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(ac);
        }


    }
}

