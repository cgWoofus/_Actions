using UnityEngine;
using System.Collections;
using UIANamespace;
public interface UIAFeedBackInterface
{
    bool IsActive();
    void windowContent(int id);
    void WindowCloseCB(WindowCloseCallBack win);
}
