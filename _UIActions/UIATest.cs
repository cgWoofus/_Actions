using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
public class UIATest : MonoBehaviour
{
    [SerializeField]
    private Rect _menuPosition;

    public GameObject _selected;
    private void OnGUI()
    {
        if (GUILayout.Button("Press Me"))
            Debug.Log("Hello!");

        GUI.Window(1, _menuPosition, windowContent,"testWindow");
    }

    void windowContent(int id)
    {
        GUILayout.Label("asdasda:");
        GUILayout.Button("option 1");

    }
}
