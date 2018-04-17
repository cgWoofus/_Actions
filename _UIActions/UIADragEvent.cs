using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class UIADragEvent : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IUIARootPositionInterface
{
    RectTransform mine;
    [SerializeField]
    Vector2 offsetPosition;
    [SerializeField]
    Rect _rect;
    bool _windowTrigger,_windowWait;
    UIAFeedBackInterface fb;
    Renderer _foo;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _windowWait = true;

        if (_foo == null)
            return;
        var fii=  _foo.sharedMaterial;
        fii.color = Color.red;
    }

    private void Awake()
    {
        mine = this.GetComponent<RectTransform>();
        fb = new TriggerAgentTool(this.gameObject);
        fb.WindowCloseCB(CloseWindow);
        _foo = this.GetComponent<Renderer>();

        if (mine == null)
            throw new NullReferenceException();
    }

    public void OnRootChange(Vector2 rootPosition)
    {
        mine.anchoredPosition = rootPosition+offsetPosition;//Camera.main.ScreenToWorldPoint(rootPosition);
        Debug.Log("calling rootChange");
    }

    void CloseWindow()
    {
        throw new NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _windowWait = false;
    }

    public bool IsActive()
    {
        return _windowWait;
    }
    
    public UIAFeedBackInterface GetMyWindow()
    {
        return fb;
    }
}

