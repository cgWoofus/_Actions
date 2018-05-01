using UnityEngine;
using TriggerNamespace;
using System;
using System.Reflection;
using System.Collections.Generic;
public static class TriggerHelper
    {

        /// <summary>
        /// Anon Method Getter from Trigger Class 
        /// </summary>
        /// <param name="MethodName"></param>
        public static TriggerHandle[] LookForListeners(this GameObject obj, string MethodName)
        {
            var _components = obj.GetComponents<Component>();
            var _flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            List<TriggerHandle> triggerHandles = new List<TriggerHandle>();
            System.Type[] _params = { };
            foreach (Component comp in _components)
            {
                var boxedComponent = comp as System.Object;
                var method = boxedComponent.GetType()
                              .GetMethod(MethodName, _flags, null, _params, null);
                if (method == null)
                    continue;
                var invokedTheMethod = method.Invoke(comp, null);
                var converTedMethod = invokedTheMethod as TriggerHandle;
                if (converTedMethod == null)
                    continue;

                triggerHandles.Add(converTedMethod);
            }

            return triggerHandles.ToArray();
        }
    }



