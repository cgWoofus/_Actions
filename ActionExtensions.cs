using UnityEngine;
using System.Collections;

internal static class NewBehavioActionExtensionsurScript1
{

    /// <summary>
    /// Extracts an interface from an <see cref="Object"/>.
    /// </summary>
    /// <typeparam name="T">Interface type to extract.</typeparam>
    /// <param name="self"><see langword="this"/>.</param>
    /// <returns>Valid reference on success; <see langword="null"/> otherwise.</returns>
    public static T GetInterface<T>(this Object self) where T : class
    {
        var result = self as T;


        if (result != null)
        {
            return result;
        }


        // Deal with GameObjects.
        var gameObject = self as GameObject;


        if (gameObject != null)
        {
            result = gameObject.GetComponent<T>();
        }


        // Warn on error.
        if (self != null && result == null)
        {
            Debug.LogWarning(self + " doesn't expose requested interface of type \"" + typeof(T) + "\".");
        }


        return result;
    }

    public static Object ToNullUnlessImplementsInterface<T>(this Object self) where T : class
    {
        var exposesInterface = self.ImplementsInterface<T>();

        // Warn on error.
        if (self != null && !exposesInterface)
        {
            Debug.LogWarning(self + " doesn't expose requested interface of type \"" + typeof(T) + "\".");
        }


        return (exposesInterface)
            ? self
            : null;
        
    }

    /// <summary>
    /// Checks whether a <see cref="Object"/> implements an interface.
    /// </summary>
    /// <typeparam name="T">Interface type to check against.</typeparam>
    /// <param name="self"><see langword="this"/>.</param>
    /// <returns><see langword="true"/> if interface is exposed; <see langword="false"/> otherwise.</returns>
    public static bool ImplementsInterface<T>(this Object self)
    {
        // Return early in case argument matches type.
        if (self is T)
        {
            return true;
        }


        // Search in components in case object is a GameObject.
        var gameObject = self as GameObject;


        if (gameObject != null)
        {
            var components = gameObject.GetComponents<T>();
            return components.Length > 0;
        }


        // Return on fail.
        return false;
    }
}