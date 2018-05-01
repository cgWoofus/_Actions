[System.Serializable]
public class TriggerObject
{
    
    public string _name;
    public float _time;
    public float _timeOffset;
    public string _triggerController;

    public string _Name { get { return _name; } set { _name = value; } }
    public float _Time {get { return _time; } set { _time = value; } }
    public float _TimeOffset { get { return _timeOffset; } set { _timeOffset = value; } }
    public string _TriggerController { get { return _triggerController; } set { _triggerController = value; } }


}

