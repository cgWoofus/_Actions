using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
public class Manager 
{
    List<System.Object> _listeners = new List<object>();

    public void AddListener(System.Object listener)
    {
        if (listener != null)
            _listeners.Add(listener);
    }

    public void SendConnectedToServerMessages(IUIARootPositionInterface data)
    {
        //  Search non-static methods both public and non-public
        BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        //  Parameters of the method to search for
        Type[] paramTypes = { typeof(IUIARootPositionInterface) };

        //  Parameter value to be passed to the method
        System.Object[] paramValues = { data };


        foreach (System.Object obj in _listeners)
        {
            //  Search for the method with signature OnConnectedToServer( ConnectionData )
            MethodInfo methodInfo = obj.GetType()
                                       .GetMethod("OnRootChange", flags, null, paramTypes, null);
            //  Call the method with parameters
            if (methodInfo != null)
                methodInfo.Invoke(obj, paramValues);
        }
    }
}