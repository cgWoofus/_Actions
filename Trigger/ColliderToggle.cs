using UnityEngine;
using System.Collections;
using TriggerNamespace;
public class ColliderToggle : MonoBehaviour, ICustomComponents
{
    public TriggerHandle GetAction()
    {
        return OnAction;
    }

    public void OnAction(bool _value)
    {
        var colliders = this.gameObject.GetComponentsInChildren<Collider2D>();
        if (colliders == null || colliders.Length == 0)
            return;
        for(int x=0;x<colliders.Length;x++)
        {
            colliders[x].enabled = !colliders[x].enabled;
        }

    }
}
