using UnityEngine;
using System;
using UIANamespace;

class SwitchAgentTool : IUIAFeedBackInterface, IUIADropObjectHandler
{
    public ObjectDropBox _dropBox;
    protected string _name;
    GameObject _target;

    public void CheckIfExisting(GameObject _globalList)
    {
        var obj = GameObject.FindObjectOfType<ActionsListener>();
    }

    public SwitchAgentTool(GameObject _obj)
    {
        _target = _obj;
        _name = _obj.name;
    }

    public void DropHandle(UnityEngine.Object obj)
    {
        throw new NotImplementedException();
    }

    public Vector2 GetBoxPosition()
    {
        return new Vector2(100, 100);
    }

    public Vector2 GetBoxSize()
    {
        return new Vector2(65, 100);
    }

    public ArgsCallBack[] GetClosingInstructions()
    {
        return null;
    }

    public Sprite GetImage()
    {
        var img = Resources.Load("_sprites/box") as Texture2D;
        var chu = Sprite.Create(img, new Rect(0f, 0f, img.width, img.height), new Vector2(0.5f, 0.5f));
        return chu;
    }

    public ArgsCallBack[] GetOpeningInstructions()
    {
          System.Object[] chu = { _dropBox as System.Object };
          ArgsCallBack[] foo = { (System.Object[] bam) => { _dropBox.ReRouteBox(bam[0]); } };
         return foo;
       // return null;
    }

    public bool IsActive()
    {
        throw new NotImplementedException();
    }

    public void OpeningCB(params ArgsCallBack[] win)
    {
        //im using a drop ui
        _dropBox = ObjectDropBox.Construct(this);
        foreach (ArgsCallBack arg in win)
            arg(this);
    }

    public void WindowCloseCB(params ArgsCallBack[] win)
    {
    }

    public void WindowContent(int id)
    {

        GUILayout.BeginHorizontal();
        GUILayout.Label("Switch:");
        GUILayout.EndHorizontal();
    }

    public string WindowName()
    {
        return "sample";
    }

    public Rect WindowShape()
    {
        var rec = Camera.main.pixelRect;
        var x = rec.width - (rec.width / 5);
        var y = rec.height / 3;
        var _staticMenuPosition = new Rect(new Vector2(x, y), new Vector2(50f,50f));

        return _staticMenuPosition;
    }

    public string GetClientName()
    {
        return _name;
    }
}

