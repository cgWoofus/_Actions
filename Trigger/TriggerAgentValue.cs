using UnityEngine;
using System.Collections;
using CustomActions;

/// <summary>
/// added to object to include trigger function on the selected object
/// </summary>
public class TriggerAgentValue : MonoBehaviour
{

    float _time, _offsetTime;
    string _name;
    public string Name{ set {_name = value; } get { return _name; } }
    TriggerObjectSend Sender;
    [SerializeField]
    TriggerObjects _tO =new TriggerObjects();
    
    public void SetObject(TriggerObjects ob)
    {
        _tO = ob;
    }
    /// <summary>
    /// Send data to the actions collector
    /// </summary>
    void Submit()
    {
        Sender(_tO);
    }

    private void OnEnable()
    {
        //subscribe to global actions listener
        var foo =  GlobalToolBox.Instance.GetOrAddComponent<ActionsListener>();
        foo.Hook(ref Sender);
        foo.OnBeforeSaveCallBackHook(Submit);
    }

    private void OnDisable()
    {
        //unsub to global actions listener
        if (GlobalToolBox.Instance == null)
            return;
        var foo = GlobalToolBox.Instance.GetOrAddComponent<ActionsListener>();
        if (foo == null)
            return;

        foo.OnBeforeSaveCallBackUnHook(Submit);

    }
}
