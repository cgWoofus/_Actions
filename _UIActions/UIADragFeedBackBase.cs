using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class UIADragFeedBackBase : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    protected RectTransform mine;
    [SerializeField]
    protected Vector2 offsetPosition;

    [SerializeField]
    protected Rect _rect;
    protected bool _windowTrigger,_windowWait;

    private  IUIAFeedBackInterface fb;
    private List<IUIAFeedBackInterface> fbCollection = new List<IUIAFeedBackInterface>();
   // Rect _cameraRect;

    [SerializeField]
    protected float _xOff,_yOff;

    protected GameObject _targetObject;



    Renderer _foo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _windowWait = true;

        if (_foo == null)
            return;
        var fii=  _foo.sharedMaterial;
        fii.color = Color.red;
    }

    protected virtual IUIAFeedBackInterface CreateFeedBackInterface()
    {
        return null;
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
        var _cameraRect = Camera.main.pixelRect;
        var _recx = _xOff * _cameraRect.width;
        var _recy = _yOff * _cameraRect.height;
        offsetPosition = new Vector2(_recx, _recy);
        mine.position = rootPosition+offsetPosition;
    }

    protected virtual void CloseWindow()
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
    
    public IUIAFeedBackInterface GetMyWindow(GameObject targetObject)
    {
        _targetObject = targetObject;

        for(int x = 0; x< fbCollection.Count;x++)
        {
            if (fbCollection[x].GetClientName() == targetObject.name)
                return fbCollection[x];
        }

        var newcol = CreateFeedBackInterface();
        fbCollection.Add(newcol);

        //  if (fb == null)
        //   fb = CreateFeedBackInterface();

        return newcol;
    }

    public void SendObject(GameObject _obj)
    {
    }
}

