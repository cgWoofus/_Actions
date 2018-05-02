using UnityEngine;
public interface IUIARootPositionInterface
{
    void OnRootChange(Vector2 rootPosition);
    bool IsActive();
    IUIAFeedBackInterface GetMyWindow(GameObject targetObject);
    void SendObject(GameObject _obj);
}
