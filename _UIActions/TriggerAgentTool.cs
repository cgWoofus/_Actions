using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UIANamespace;
using UnityEngine.EventSystems;

using System.Reflection;
public class TriggerAgentTool : IUIAFeedBackInterface ,IUIADropObjectHandler
{
    float _time, _timeOffset;
    string _timeStr = "", _offsetStr = "";
    string _triggerController = "n/a";
    string _name = "n/a";
    const string _imageSource = "_sprites/box";


    public ObjectDropBox _dropBox;
    #region TriggerObject Property copy
    public string _Name { get { return _name; } set { _name = value; } }
    public float _Time { get { return _time; } set { _time = value; } }
    public float _TimeOffset { get { return _timeOffset; } set { _timeOffset = value; } }
    public string _TriggerController { get { return _triggerController; } set { _triggerController = value; } }

    #endregion

    private Rect _staticMenuPosition;
    Vector2 infi = new Vector2(999f, 999f);
    Vector2 size = new Vector2(150, 300);

    public Rect StaticMenuPosition
    {
        get
        {
            return _staticMenuPosition;
        }

        set
        {
            _staticMenuPosition = value;
        }
    }



    bool _doOnce;
    GameObject _target;
    ArgsCallBack winClose;

    private Rect dropTargetRect = new Rect(30.0f, 80.0f, 30.0f, 30.0f);

    TriggerObject _tO = new TriggerObject();
    public bool IsActive()
    {
        throw new NotImplementedException();
    }

    public TriggerAgentTool(GameObject _obj)
    {
        _target = _obj;
        _name = _obj.name;
    }


    public void CheckIfExisting(GameObject _globalList)
    {
        var chu = _globalList.transform.GetOrAddComponent<ActionsListener>();
        var foo = chu.CheckAvailability(_target.name);

        if (foo != null)
        {
            _time = foo._Time;
            _timeOffset = foo._TimeOffset;
            _triggerController = foo._triggerController;
            _timeStr = _time.ToString();
            _offsetStr = _timeOffset.ToString();
        }
    }

    public void WindowContent(int id)
    {

        GUILayout.BeginHorizontal();
        GUILayout.Label("Time:");

        _timeStr = GUILayout.TextField(_timeStr, 4);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Offset");
        _offsetStr = GUILayout.TextField(_offsetStr, 4);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label(_triggerController);
        GUILayout.EndHorizontal();



        if (GUILayout.Button("Apply"))
        {
            Apply();
        }


    }

    void Apply()
    {
        float.TryParse(_timeStr, out _time);
        float.TryParse(_offsetStr, out _timeOffset);
        _Name = _target.name;
        var t = _target.transform.GetOrAddComponent<TriggerAgentValue>();
        EditTriggerObject();
        t.SetObject(_tO);
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

            if (des == null)
                continue;
            des.SetValue(_tO,
                val,
                null);
            //   Debug.Log(des.Name);
        }
    }

    public void WindowCloseCB(params ArgsCallBack[] win)
    {
    }

    public void ReRouteObject(System.Object newObject)
    {
        _dropBox = newObject as ObjectDropBox;
        if (_dropBox != null)
            _dropBox.ReRouteBox(this);
    }

    public string WindowName()
    {
        return _name;
    }

    public Rect WindowShape()
    {
        var rec = Camera.main.pixelRect;
        var x = rec.width - (rec.width / 5);
        var y = rec.height / 3;
        _staticMenuPosition = new Rect(new Vector2(x, y), size);

        return _staticMenuPosition;
    }

    public ArgsCallBack[] GetClosingInstructions()
    {
        return null;
    }

    /// <summary>
    /// Passing of parameters to a new instance 
    /// </summary>
    /// <returns></returns>
    public ArgsCallBack[] GetOpeningInstructions()
    {
        System.Object[] chu = { _dropBox as System.Object };
        ArgsCallBack[] foo = { (System.Object[] bam) => { _dropBox.ReRouteBox(bam[0]); } };
        return foo;
    }

    public void OpeningCB(params ArgsCallBack[] win)
    {
        //im using a drop ui
        _dropBox = ObjectDropBox.Construct(this);
        foreach (ArgsCallBack arg in win)
            arg(this);
    }

    public Sprite GetImage()
    {
        var img = Resources.Load(_imageSource)as Texture2D;
        var chu = Sprite.Create(img, new Rect(0f, 0f, img.width, img.height),new Vector2(0.5f,0.5f));
        return chu;
    }
     
    public Vector2 GetBoxSize()
    {
        return new Vector2(100, 100);
    }

    public Vector2 GetBoxPosition()
    {
        return new Vector2(65, 100);
    }

    /// <summary>
    /// Objects dropped on the box would be examined here
    /// </summary>
    /// <param name="obj"></param>
    public void DropHandle(UnityEngine.Object obj)
    {
        var gameObject = obj as GameObject;
        if (obj == null)
            return;
        _triggerController = gameObject.name;
    }

}
