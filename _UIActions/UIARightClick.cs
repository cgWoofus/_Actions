using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class UIARightClick : MonoBehaviour, IBeginDragHandler ,IEndDragHandler
{

    [SerializeField]
    private Rect _menuPosition = new Rect(Vector2.zero, new Vector2(150f, 100f));
    private bool _trigger = false, _subWindowTrigger = false;
    private Rect _currentWindow;
    Vector2 infi = new Vector2(999f,999f);
    Vector2 size = new Vector2(150, 100);
    [SerializeField]
    IUIAFeedBackInterface fb;
    public delegate void UIcallBackHandle(IUIAFeedBackInterface id);
    public UIcallBackHandle UpdateWindow;


    static IList Chanz;

    public static IList ListOfSomething { set { Chanz = value; } get { return Chanz; } }


    void Start()
    {
        fb = new TriggerAgentTool(this.gameObject);
        fb.WindowCloseCB(CloseWindow);
    }   
    
    void CloseWindow()
    {
        _subWindowTrigger = false;
        _trigger = false;
    }

    public void MenuPositionUpdate(Vector2 position)
    {
        var pos = new Vector2(position.x, Camera.main.pixelRect.height - position.y);
        _menuPosition = new Rect(pos, _menuPosition.size);
        UpdateUIListenersPosition(position);
    }

    private static void UpdateUIListenersPosition(Vector2 position)
    {
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
                foo.OnRootChange(position);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            MenuPositionUpdate(eventData.position);
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
                            foo.SendObject(this.gameObject);
                            if (fb == null)
                                fb = foo.GetMyWindow();
                            UpdateWindow(fb);
                            break;
                        }
                    }
                }

            MenuPositionUpdate(infi);
        }
    }

}
