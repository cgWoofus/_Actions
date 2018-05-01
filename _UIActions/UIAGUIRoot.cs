using UnityEngine;
using System.Collections;

public class UIAGUIRoot : MonoBehaviour
{
    private Rect _staticMenuPosition;
    [SerializeField]
    IUIAFeedBackInterface currentFeedBackContent;
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
        if(currentFeedBackContent!=null)
           GUILayout.Window(1, currentFeedBackContent.WindowShape(), currentFeedBackContent.WindowContent, currentFeedBackContent.WindowName());
    }

    public void UpdateWindow(IUIAFeedBackInterface newFeedBackContent)
    {

        if (currentFeedBackContent != null)
            {
                //get objects  from closing object
                var fd = currentFeedBackContent.GetOpeningInstructions();
                currentFeedBackContent.WindowCloseCB();
                newFeedBackContent.OpeningCB(fd);
            }
        else
            newFeedBackContent.OpeningCB();

        currentFeedBackContent = newFeedBackContent;
    }






}
