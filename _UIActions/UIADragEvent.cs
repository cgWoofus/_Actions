using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class UIADragTrigger : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IUIARootPositionInterface
{
    RectTransform mine;
    [SerializeField]
    Vector2 offsetPosition;

    [SerializeField]
    Rect _rect;
    bool _windowTrigger,_windowWait;
    IUIAFeedBackInterface fb;
    Rect _cameraRect;

    [SerializeField]
    float _xOff,_yOff;

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
        _foo = this.GetComponent<Renderer>();

        if (mine == null)
            throw new NullReferenceException();




    }

    public void OnRootChange(Vector2 rootPosition)
    {
        _cameraRect = Camera.main.pixelRect;
        var _recx = _xOff * _cameraRect.width;
        var _recy = _yOff * _cameraRect.height;
        offsetPosition = new Vector2(_recx, _recy);
        mine.position = rootPosition+offsetPosition;
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
    
    public IUIAFeedBackInterface GetMyWindow()
    {
        return fb;
    }

    public void SendObject(GameObject _obj)
    {
        if(fb == null)
        { 
            fb = new TriggerAgentTool(_obj);
            fb.WindowCloseCB(CloseWindow);
        }
    }
}

