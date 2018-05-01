using UnityEngine;
using TriggerNamespace;
using System;
using System.Reflection;
using System.Collections.Generic;
/// <summary>
/// Switch sample behaviour
/// </summary>
public class ObjectSwitch : MonoBehaviour,ITSwitch, ICustomComponents
{

    TriggerHandle mainTriggerHandle;
    event TriggerHandle SwitchEvents;
    
    /// <summary>
    /// message to be sent to the trigger
    /// </summary>
    /// <param name="value"></param>
    public void Go(bool value)
    {
        if(mainTriggerHandle != null)
            mainTriggerHandle(value);
    }

    private void Start()
    {
        var listeners = this.gameObject.LookForListeners("GetAction");//LookForListeners("GetAction");
        foreach(TriggerHandle handle in listeners)
            SwitchEvents += handle;
    }

    public void Behave(bool arg)
    {
        //if korgi is on top of the switch
            Go(arg);

        if (SwitchEvents != null)
            SwitchEvents(arg);
        //call Go(true)
        //OR create a Timerbehaviour
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag== "Player")
        {
            Behave(true);
            return;

        }
        throw new NotImplementedException();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Behave(false);
            return;
        }
        throw new NotImplementedException();
    }

    public void SetHandle(TriggerHandle handle)
    {
        mainTriggerHandle = handle;
    }

}

