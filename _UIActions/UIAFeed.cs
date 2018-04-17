using UnityEngine;
using System.Collections;
using System;

public class UIAFeed : MonoBehaviour, TriggerInterface
{
    [SerializeField]
    private Rect _menuPosition = new Rect(Vector2.zero, new Vector2(150f, 100f));
    private bool _trigger=false,_subWindowTrigger=false;


    [SerializeField]
    UIAFeedBackInterface fb;
    public delegate void UIcallBackHandle(int id);
    public UIcallBackHandle windowOne;
    // Use this for initialization
    void Start()
    {
        fb = new TriggerAgentTool(this.gameObject);
        fb.WindowCloseCB(CloseWindow);
    }

    private void OnGUI()
    {
        if(_trigger)
        {
            GUI.Window(1, _menuPosition, WindowContent, "testWindow");
        }


        if (_subWindowTrigger)
        {
            var _newPosition = new Rect(new Vector2(_menuPosition.x+_menuPosition.width, _menuPosition.y), _menuPosition.size);
            GUI.Window(2, _newPosition, fb.windowContent, "testWindow");
        }
    }

    void WindowContent(int id)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Set Trigger:");
        if(GUILayout.Button("Set"))
        {
            _subWindowTrigger = !_subWindowTrigger;
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Set Action:");
        if (GUILayout.Button("Set"))
        {
            throw new NotImplementedException();
        }
        
        GUILayout.EndHorizontal();

        //
    }

    void CloseWindow()
    {
        _subWindowTrigger = false;
        _trigger = false;
    }


    


    void ShowSubWindow(int id)
    {
        GUILayout.Label("Test");
    }


    public void TeeAction(GameObject _target)
    {
        _target.transform.GetOrAddComponent<UIAFeed>();
    }

    public void Trigger(bool val)
    {
        _trigger = !_trigger;
    }

    public void MenuPositionUpdate(Vector2 position)
    {
        var pos = new Vector2(position.x, Camera.main.pixelRect.height - position.y);
        _menuPosition = new Rect(pos, _menuPosition.size);
    }
}
