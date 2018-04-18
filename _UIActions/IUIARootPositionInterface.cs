using UnityEngine;
public interface IUIARootPositionInterface
{
    void OnRootChange(Vector2 rootPosition);
    bool IsActive();
    IUIAFeedBackInterface GetMyWindow();
    void SendObject(GameObject _obj);
}
