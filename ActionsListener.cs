using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections;
using CustomActions;

/// <summary>
/// using preparesave interface
/// </summary>
public class ActionsListener : MonoBehaviour ,PrepareSaveInterface
{
    public static event SerializeAction OnSave;
    [SerializeField]
    ActionsCollector _acObject;

    private void Awake()
    {
        var global = GlobalToolBox.Instance;
        var CollectionPath = global._myComp._globalVariableContainer._specialCollectionPath;
        var chu = AssetDatabase.LoadAssetAtPath(CollectionPath, typeof(ActionsCollector));
        if (chu != null)
            _acObject = chu as ActionsCollector;
    }

    /// <summary>
    /// if the object is non existent add a new one to the list
    /// </summary>
    /// <param name="b"></param>
    void Addtolist(TriggerObject b)
    {
        var list = _acObject._objects;
        var ExistingObject = TriggerObjectCheckAvailability(b._Name);
        if(ExistingObject!=null)
        {
            UpdateObject(ExistingObject, b);
            return;
        }

        _acObject._objects.Add(b);
    }

    /// <summary>
    /// Update Trigger Object data replacing existing one
    /// </summary>
    /// <param name="old"></param>
    /// <param name="noo"></param>
    void UpdateObject(TriggerObject old, TriggerObject noo)
    {
        foreach (PropertyInfo sourceProperty in noo.GetType().GetProperties())
        {
            PropertyInfo des = old.GetType().GetProperty(sourceProperty.Name);

            des.SetValue(
                old,
                sourceProperty.GetValue(noo, null),
                null);
        }

    }

    public void Hook(ref TriggerObjectSend foo)
    {
        foo = Addtolist;
    }

    public void OnBeforeSaveCallBackHook(SerializeAction foo)
    {
        OnSave += foo;
    }

    public void OnBeforeSaveCallBackUnHook(SerializeAction foo)
    {
        OnSave -= foo;
    }

    public void OnBeforeSaveExecute()
    {
        if (OnSave != null)
        {
            OnSave();
            EditorUtility.SetDirty(_acObject);
        }
    }


    public TriggerObject TriggerObjectCheckAvailability(string _objectName)
    {
        var list = _acObject._objects;
        for (int x = 0; x < list.Count; x++)
        {
            if (_objectName == list[x]._Name)
            {
                return list[x];
            }

        }
        return null;
    }

}

