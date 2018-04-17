using UnityEngine;
using UnityEngine.Events;
public delegate void OnValueChange<T>(T val);
public interface ITriggerInterface
{
     bool GetTriggerResult();
     bool IsActive();
     void OnValueChangeHook(UnityAction<bool> b);
     void OnValueChangeEvent(OnValueChange<bool> func);
}