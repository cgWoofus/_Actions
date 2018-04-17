using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class UIAListener : MonoBehaviour
{
    EventTrigger.Entry entree;
    void Start()
    {
        var eve = transform.GetOrAddComponent<EventTrigger>();
        // var entree = new EventTrigger.Entry();
        entree = new EventTrigger.Entry();
        var exit = new EventTrigger.Entry();
        entree.eventID = EventTriggerType.PointerClick;
        // exit.eventID = EventTriggerType.PointerExit;
        entree.callback.AddListener((data) => { Activate((PointerEventData)data); });
        eve.triggers.Add(entree);



    }

    void Activate(PointerEventData data)
    {

        this.gameObject.transform.GetOrAddComponent<BCAgent>();
        entree.callback.RemoveListener((chu) => { Activate(data); });
        
        
    }
}
