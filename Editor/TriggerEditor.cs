using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Trigger))]
public class TriggerEditor : Editor
{
    #region FieldNames
    protected const string _triggerHandleSource = "_TriggerHandleObject";
    protected const string _triggerValueSource = "_trigger";
    #endregion



    public override void OnInspectorGUI()
    {
        var trigger = target as Trigger;

        if (trigger == null)
            return;

        EditorGUI.BeginChangeCheck();

        base.OnInspectorGUI();

        trigger.ControllerObject = EditorGUILayout.ObjectField("Trigger Object", trigger.ControllerObject, typeof(Object), true);
        GUIContent s = new GUIContent();
        s.text = string.Format("Trigger Status: {0}", trigger._Trigger);
        EditorGUILayout.LabelField(s);


        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(trigger);

    }

}