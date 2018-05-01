using UnityEngine;
using System.Collections;

public class TriggerAgent : MonoBehaviour
{

    // Use this for initialization
    TriggerData _myTriggerData;
    [SerializeField]
    float _time,_curTime, _timeOffset;
    [SerializeField]
    GameObject _triggerObject;
    public string _triggerController;

    float _tick;
    event OnValueChange<bool> TriggerChange;

    public float _Time { get { return _time; } set { _time = value; } }
    public float _TimeOffset { get { return _timeOffset; } set { _timeOffset = value; } }
    public string _TriggerController { get { return _triggerController; } set { _triggerController = value; } }

    void Awake()
    {
        SetUpTriggerData();
        //_curTime = Time.time;
        //_tick = 0f;
    }

    private void Start()
    {
        SetupTriggerCondition();
    }

    public void SetupTriggerCondition()
    {
        _triggerObject = GetTriggerObject(_triggerController);
        if (_triggerObject == null)
            TriggerTimer.Construct(this.gameObject, _time, _timeOffset, _myTriggerData.GO);
        else
            TriggerSwitchBase.Construct(_triggerObject, _myTriggerData.GO);
    }

    private void SetUpTriggerData()
    {
        var Trigger = transform.GetOrAddComponent<Trigger>();
        if (_myTriggerData == null)
            _myTriggerData = transform.GetOrAddComponent<TriggerData>();
        Trigger.ControllerObject = _myTriggerData;
    }

    public GameObject GetTriggerObject(string objectName)
    {
        if (objectName == "n/a" || objectName == "")
            return null;

        if (_triggerObject != null)
            return _triggerObject;

        return GameObject.Find(objectName);
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
