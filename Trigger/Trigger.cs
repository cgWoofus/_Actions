using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Trigger : MonoBehaviour
{

    public delegate void  TriggerHandle(bool b);

    public event TriggerHandle TriggerEvent;


    [SerializeField,HideInInspector]
    private Object _TriggerHandleObject;


    private bool _trigger;
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

    public bool _Trigger { get {return _trigger; }}

    private void Start()
    {
        _targetInterface = TargetInterface;
        if (_targetInterface == null)
            return;
        UnityAction<bool> chucchu = OnValueChanged;        
        _targetInterface.OnValueChangeHook(chucchu);
             
    }

    private void LateUpdate()
    {
    }

    void OnValueChanged(bool change)
    {
        if (_trigger == change)
            return;
        _trigger = change;
        if (TriggerEvent != null)
            TriggerEvent(change);
    }

    public void eventHook(TriggerHandle chu)
    {
        TriggerEvent -= chu;
        TriggerEvent += chu;
    }


}
