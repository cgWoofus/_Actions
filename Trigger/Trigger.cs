using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using TriggerNamespace;

public class Trigger : MonoBehaviour , ICustomComponents
{


    public event TriggerHandle TriggerEvent;


    [SerializeField,HideInInspector]
    private Object _TriggerHandleObject;


    private bool _trigger;
    private int _subscribers; 
    private ITriggerInterface _targetInterface;

    private ITriggerInterface TargetInterface
    {
        get
        {
            if (_targetInterface == null)
                _targetInterface = ControllerObject.GetInterface<ITriggerInterface>();
            return _targetInterface;

        }

    }

    public Object ControllerObject {
        set { _TriggerHandleObject = value.ToNullUnlessImplementsInterface<ITriggerInterface>();}
        get { return _TriggerHandleObject; }
    }
    public int _NumberOfSubs { get { return _subscribers; } }

    public bool _Trigger { get {return _trigger; }}

    private void Start()
    {
        _targetInterface = TargetInterface;
        if (_targetInterface == null)
            return;
        UnityAction<bool> chucchu = OnValueChanged;        
        _targetInterface.OnValueChangeHook(chucchu);
        _subscribers = 0;
        LookForListeners("GetAction");

             
    }

    void OnValueChanged(bool change)
    {
        if (_trigger == change)
            return;
        _trigger = change;
        if (TriggerEvent != null)
            TriggerEvent(change);
    }

    public void EventHook(TriggerHandle chu)
    {
        TriggerEvent -= chu;
        TriggerEvent += chu;
        _subscribers++;
    }


    public void LookForListeners(string MethodName)
    {
       var _components = this.gameObject.GetComponents<Component>();
       var _flags = BindingFlags.Instance | BindingFlags.Public| BindingFlags.NonPublic;
       System.Type[] _params = {};
        foreach (Component comp in _components)
        { 
            var boxedComponent = comp as System.Object;
            var method = boxedComponent.GetType()
                          .GetMethod(MethodName, _flags,null, _params,null);
            if (method == null)
                continue;
            var invokedTheMethod = method.Invoke(comp, null);
            var converTedMethod = invokedTheMethod as TriggerHandle;
            if (converTedMethod == null)
                continue;
            EventHook(converTedMethod);
        }

    }


}
