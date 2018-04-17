using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;


[System.Serializable]
public class TriggerData :MonoBehaviour, ITriggerInterface
{
    bool _active;

    [SerializeField]
    TriggerValueChangeEvent TriggerValueChange;

    [SerializeField]
    bool _trigger;

    event OnValueChange<bool> TriggerChange;


    public bool _ActiveSetter
    {
        set { value = _active; }
        get { return _active; }
    }

    public bool GetTriggerResult()
    {
        throw new NotImplementedException();
    }
    
    public bool IsActive()
    {
        return _active;
    }

    /// <summary>
    /// Trigger Toggle ON/OFF
    /// </summary>
    public void GO()
    {
        _trigger = !_trigger;

        if (TriggerChange != null)
            TriggerChange(_trigger);

        if (TriggerValueChange != null)
            TriggerValueChange.Invoke(_trigger);

    }

    public void OnValueChangeEvent(OnValueChange<bool> func)
    {
        throw new NotImplementedException();
    }

    public void OnValueChangeHook(UnityAction<bool> b)
    {
        if (TriggerValueChange == null)
            TriggerValueChange = new TriggerValueChangeEvent();
        TriggerValueChange.AddListener(b);
    }
}
