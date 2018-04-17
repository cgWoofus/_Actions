using UnityEngine;
using System.Collections;
using System;
using UIANamespace;


using System.Reflection;
public class TriggerAgentTool : UIAFeedBackInterface
{
    float _time,_timeOffset;
    string _timeStr="",_offsetStr="";
    string _triggerController = "n/a";
    string _name="n/a";


    public string _Name { get { return _name; } set { _name = value; } }
    public float _Time { get { return _time; } set { _time = value; } }
    public float _TimeOffset { get { return _timeOffset; } set { _timeOffset = value; } }
    public string _TriggerController { get { return _triggerController; } set { _triggerController = value; } }

    GameObject _target;
    WindowCloseCallBack winClose;
    TriggerObjects _tO=new TriggerObjects();
    public bool IsActive()
    {
        throw new NotImplementedException();
    }

    public TriggerAgentTool(GameObject _obj)
    {
        _target = _obj;
        var chu = GlobalToolBox.Instance.GetOrAddComponent<ActionsListener>();
        var foo = chu.CheckAvailability(_obj.name);
        if(foo!=null)
        {
            _time = foo._Time;
            _timeOffset = foo._TimeOffset;
            _timeStr = _time.ToString();
            _offsetStr = _timeOffset.ToString();
        }

    }

    public void windowContent(int id)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Time:");

        _timeStr = GUILayout.TextField(_timeStr,4);
        
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Offset");

        _offsetStr =  GUILayout.TextField(_offsetStr,4);
        GUILayout.EndHorizontal();

        if(GUILayout.Button("Apply"))
        {
            Apply();
        }


    }

    void Apply()
    {
        float.TryParse(_timeStr,out _time);
        float.TryParse(_offsetStr, out _timeOffset);
        _Name = _target.name;       
        var t = _target.transform.GetOrAddComponent<TriggerAgentValue>();
        EditTriggerObject();
        t.SetObject(_tO);      
        winClose();
    }

    /// <summary>
    /// copy all available matching properties
    /// </summary>
    void EditTriggerObject()
    {
        
        foreach (PropertyInfo source in this.GetType().GetProperties())
        {
            PropertyInfo des = _tO.GetType().GetProperty(source.Name);
            var val = source.GetValue(this, null);
            des.SetValue(_tO,
                val,
                null);
            Debug.Log(des.Name);
        }
        
    }

    public void WindowCloseCB(WindowCloseCallBack win)
    {
        winClose = win;
    }
}
