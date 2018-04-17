using UnityEngine;
using System.Collections;
public class TriggerAgent : MonoBehaviour
{

    // Use this for initialization
    TriggerData _myTriggerData;
    [SerializeField]
    float _time,_curTime, _timeOffset;

    float _tick;
    event OnValueChange<bool> TriggerChange;

    public float _Time { get { return _time; } set { _time = value; } }
    public float _TimeOffset { get { return _timeOffset; } set { _timeOffset = value; } }

    void Awake()
    {
        // register to globalListener
        var Trigger = this.transform.GetOrAddComponent<Trigger>();
        if (_myTriggerData == null)
        {
            _myTriggerData = transform. GetOrAddComponent<TriggerData>();
        }
        Trigger.ControllerObject = _myTriggerData;
        _curTime = Time.time;
        _tick = 0f;
       
    }

    private void Update()
    {
        var tme = _time + _timeOffset;
        _tick = _tick < tme ? Time.time - _curTime : resetTimer();
    }


    float resetTimer()
    {
        _curTime = Time.time;
        _timeOffset = 0f;
        if (_myTriggerData != null)
            _myTriggerData.GO();

        return 0f;
    }


    private void OnEnable()
    {
        if (_myTriggerData == null)
            {
                _myTriggerData = transform.GetOrAddComponent<TriggerData>();
            }
        _myTriggerData._ActiveSetter = isActiveAndEnabled;
    }

    private void OnDisable()
    {
        _myTriggerData._ActiveSetter = isActiveAndEnabled;
    }
}
