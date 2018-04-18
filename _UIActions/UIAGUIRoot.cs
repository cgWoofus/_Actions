using UnityEngine;
using System.Collections;

public class UIAGUIRoot : MonoBehaviour
{
    private Rect _staticMenuPosition;
    [SerializeField]
    IUIAFeedBackInterface feedBackContent;
    Vector2 infi = new Vector2(999f, 999f);
    Vector2 size = new Vector2(150, 100);
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

    void Start()
    {
        var rec = Camera.main.pixelRect;
        var x = rec.width - (rec.width / 5);
        var y = rec.height / 3;
        _staticMenuPosition = new Rect(new Vector2(x, y), size);
    }

    private void OnGUI()
    {
        if(feedBackContent!=null)
           GUILayout.Window(1, feedBackContent.WindowShape(), feedBackContent.WindowContent, feedBackContent.WindowName());
    }

    public void UpdateWindow(IUIAFeedBackInterface _content)
    {
        feedBackContent = _content;
    }






}
