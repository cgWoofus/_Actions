using UnityEngine;
using System.Collections;
using UIANamespace;
public interface IUIAFeedBackInterface
{
    bool IsActive();
    void WindowContent(int id);
    Rect WindowShape();
    string WindowName();
    void WindowCloseCB(WindowCloseCallBack win);
}
