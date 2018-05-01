using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class ActionsCollector : ScriptableObject
{    
    [SerializeField]
    public List<TriggerObject> _objects;

    [SerializeField]
    public List<SwitchObject> _switchObjects;


}

