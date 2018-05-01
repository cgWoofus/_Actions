using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Reflection;
using UnityEngine.UI;
public class ObjectDropBox : MonoBehaviour, IDropHandler , IPointerEnterHandler, IPointerExitHandler
{
    GameObject _target;
    const string _dropHandleMethod = "DropHandle";

    Color32 colorDrop = new Color32(0, 255, 0, 255);
    Color32 colorNormal;
    delegate void ObjectDropEventHandle(UnityEngine.Object obj);
    ObjectDropEventHandle handleInstance;
    static ObjectDropBox _myInstance;
    public GameObject Target
    {
        get
        {
            return _target;
        }

        set
        {
            _target = value;
        }
    }

    public System.Object HandleInstance
    {
        get
        {
            return handleInstance as System.Object;
        }

    }

    public static ObjectDropBox MyInstance
    {
        get
        {
            return _myInstance;
        }

        set
        {
            _myInstance = value;
        }
    }

    public static ObjectDropBox Construct(System.Object listener)
    {
        if (_myInstance != null)
            return _myInstance;

        var canvas = GameObject.Find("UIACanvas");
        if (canvas == null)
            return null;
        ObjectDropBox dropBox = CreateInstance(listener, canvas);
        _myInstance = dropBox;
        return dropBox;
    }

    private void Start()
    {
        colorNormal = this.GetComponent<Image>().color;
    }

    private static ObjectDropBox CreateInstance(object listener, GameObject canvas)
    {
        var dropNamer = string.Format("{0} dropbox", listener.ToString());
        var obj = new GameObject(dropNamer, typeof(RectTransform), typeof(CanvasRenderer));
        obj.transform.SetParent(canvas.transform);
        obj.transform.GetOrAddComponent<UnityEngine.UI.Image>().sprite = CreateImage(listener);

        CreateBox(listener,obj.GetComponent<RectTransform>());
        
        var dropBox = obj.transform.GetOrAddComponent<ObjectDropBox>();
        dropBox.handleInstance = ExtractMethodToDelegate(listener, _dropHandleMethod);
        return dropBox;
    }

    public static Sprite CreateImage(object listener)
    {
        var imgMethod = MethodGet(listener, "GetImage", new Type[] { });
        var imgres = imgMethod.Invoke(listener, null);
        return imgres as Sprite;
    }

    public static void CreateBox(object listener,RectTransform currentRect)
    {
        var boxSizeMethod = MethodGet(listener, "GetBoxSize", new Type[] { });
        var boxpositionMethod = MethodGet(listener, "GetBoxPosition", new Type[] { });
        if (boxSizeMethod == null || boxpositionMethod == null)
            throw new NullReferenceException();
        var boxSize = (Vector2)boxSizeMethod.Invoke(listener, null);
        var boxPosition = (Vector2)boxpositionMethod.Invoke(listener, null);

        currentRect.sizeDelta = boxSize;
        currentRect.position = boxPosition;       

    }



    static ObjectDropEventHandle ExtractMethodToDelegate(System.Object obj,string methodString)
    {
        MethodInfo methodInfo = MethodGet(obj, methodString, new Type[] { typeof(UnityEngine.Object) });
        return (ObjectDropEventHandle)Delegate.CreateDelegate(typeof(ObjectDropEventHandle), obj, methodInfo);
    }

    private static MethodInfo MethodGet(object obj, string methodName,Type[] parameterTypes)
    {
        BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        Type[] paramTypes = parameterTypes;
        MethodInfo methodInfo = obj.GetType()
                                       .GetMethod(methodName, flags, null, paramTypes, null);        
        return methodInfo;
    }

    public void ReRouteBox(System.Object listener)
    {
        handleInstance = ExtractMethodToDelegate(listener,_dropHandleMethod);
    }

    public void OnDrop(PointerEventData eventData)
    {
        //check if interactable
        if (handleInstance != null && eventData.button == PointerEventData.InputButton.Left)
            handleInstance(eventData.pointerPress);

        this.GetComponent<Image>().color = colorNormal;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging)
            this.GetComponent<Image>().color = colorDrop;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
            this.GetComponent<Image>().color = colorNormal;
    }
}

