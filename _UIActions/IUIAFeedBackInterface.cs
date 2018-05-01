using UnityEngine;
using System.Collections;
using UIANamespace;
public interface IUIAFeedBackInterface
{
    bool IsActive();
    void WindowContent(int id);
    Rect WindowShape();
    string WindowName();
    ArgsCallBack[] GetClosingInstructions();
    ArgsCallBack[] GetOpeningInstructions();
    void WindowCloseCB(params ArgsCallBack[] win);
    void OpeningCB(params ArgsCallBack[] win);
    void CheckIfExisting(GameObject _globalList);
}
