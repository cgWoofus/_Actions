using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class UIARightClick : MonoBehaviour, IBeginDragHandler ,IEndDragHandler,IPointerDownHandler, IPointerUpHandler
{

    [SerializeField]
    private Rect _menuPosition = new Rect(Vector2.zero, new Vector2(150f, 100f));
    private bool _trigger = false, _subWindowTrigger = false;

    Vector2 infi = new Vector2(999f,999f);

    [SerializeField]
    UIAFeedBackInterface fb;
    public delegate void UIcallBackHandle(int id);
    public UIcallBackHandle windowOne;

    static IList Chanz;

    public static IList ListOfSomething { set { Chanz = value; } get { return Chanz; } }


    void Start()
    {
        fb = new TriggerAgentTool(this.gameObject);
        fb.WindowCloseCB(CloseWindow);
    }

    private void OnGUI()
    {
        if (_trigger)
        {
            if(fb!=null)
                GUI.Window(1, _menuPosition, fb.windowContent, "testWindow");            
        }
      //  if (_subWindowTrigger)
       /// {
         ///   var _newPosition = new Rect(new Vector2(_menuPosition.x + _menuPosition.width, _menuPosition.y), _menuPosition.size);
         //   GUI.Window(2, _newPosition, fb.windowContent, "testWindow");
       /// }
    }

    void WindowContent(int id)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Set Trigger:");
        if (GUILayout.Button("Set"))
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

    public void MenuPositionUpdate(Vector2 position)
    {
        var pos = new Vector2(position.x, Camera.main.pixelRect.height - position.y);
        _menuPosition = new Rect(pos, _menuPosition.size);

        if(Chanz!=null||Chanz.Count>0)
        {
            for(int x=0; x< Chanz.Count;x++)
            {
                var foo = Chanz[x] as IUIARootPositionInterface;
                if(foo ==null)
                {
                    Debug.Log("does not follow interface");
                    continue;
                }

                foo.OnRootChange(position);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
          //  _trigger = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //_trigger = false;
            if (eventData.button == PointerEventData.InputButton.Right)
                if (Chanz != null || Chanz.Count > 0)
                {
                    for (int x = 0; x < Chanz.Count; x++)
                    {
                        var foo = Chanz[x] as IUIARootPositionInterface;
                        if (foo == null)
                        {
                            Debug.Log("does not follow interface");
                            continue;
                        }
                        if (foo.IsActive())
                        {
                            _trigger = foo.IsActive();
                            fb = foo.GetMyWindow();
                            break;
                        }
                    }
                }

            MenuPositionUpdate(infi);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            MenuPositionUpdate(eventData.position);
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
