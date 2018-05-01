using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using TriggerNamespace;
public class TriggerTimer : MonoBehaviour
{
    float _curTime,_tick,_time,_timeOffset;
    bool _toggle=true;
    TriggerHandle GoFunctionExecute;
    private void Awake()
    {
        _curTime = Time.time;
        _tick = 0f;
    }

    /// <summary>
    /// create and attach instance
    /// </summary>
    /// <param name="target"></param>
    /// <param name="time"></param>
    /// <param name="timeOffset"></param>
    /// <param name="GoFunction"></param>
    public static void Construct(GameObject target,float time,float timeOffset, TriggerHandle GoFunction)
    {
        var tt = target.transform.GetOrAddComponent<TriggerTimer>();
        tt._time = time;
        tt._timeOffset = timeOffset;
        tt.GoFunctionExecute = GoFunction;
    }

    private void Update()
    {
        var tme = _time + _timeOffset;
        _tick = _tick < tme ? Time.time - _curTime : ResetTimer();
    }

    float ResetTimer()
    {
        _curTime = Time.time;
        _timeOffset = 0f;
        _toggle = !_toggle;
        if (GoFunctionExecute != null)
            GoFunctionExecute(_toggle);

        return 0f;
    }



}

