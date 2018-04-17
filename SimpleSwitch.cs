using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class SimpleSwitch : MonoBehaviour, ITriggerInterface
{
    [SerializeField]
    bool _trigger;

    [SerializeField]
    TriggerValueChangeEvent TriggerValueChange;

    event OnValueChange<bool> TriggerChange;

    bool ITriggerInterface.GetTriggerResult()
    {
        return _trigger;
    }
    bool ITriggerInterface.IsActive()
    {
        return isActiveAndEnabled;
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

    public  void OnValueChangeEvent(OnValueChange<bool> func)
    {
    }

    public void OnValueChangeHook(UnityAction<bool> b)
    {        
        TriggerValueChange.AddListener(b);
    }

    

}

[Serializable]
public class TriggerValueChangeEvent : UnityEvent<bool>
{

}


