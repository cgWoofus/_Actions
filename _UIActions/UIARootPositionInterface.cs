using UnityEngine;
using System.Collections;

public interface IUIARootPositionInterface
{
    void OnRootChange(Vector2 rootPosition);
    bool IsActive();
    UIAFeedBackInterface GetMyWindow();
}
