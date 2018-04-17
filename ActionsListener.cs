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
        var chu = AssetDatabase.LoadAssetAtPath(global._myComp._globalVariableContainer._specialCollectionPath,typeof(ActionsCollector));
        if (chu != null)
            _acObject = chu as ActionsCollector;

    }

    void Addtolist(TriggerObjects b)
    {
        var list = _acObject._objects;
        var ExistingObject = CheckAvailability(b._Name);
        if(ExistingObject!=null)
        {
            UpdateObject(ExistingObject, b);
            return;
        }

        _acObject._objects.Add(b);
    }

    void UpdateObject(TriggerObjects old, TriggerObjects noo)
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


    public TriggerObjects CheckAvailability(string _objectName)
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

