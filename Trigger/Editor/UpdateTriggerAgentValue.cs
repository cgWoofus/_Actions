using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
public class UpdateTriggerAgentValue : ScriptableObject
{

    const string assetLocation = "Assets/Resources/data/editor/special.asset";

    [MenuItem("Tools/Trigger/Update Trigger Agent")]
    static void DoIt()
    {
        UpdateTriggerAgent();
    }

    public static void UpdateTriggerAgent()
    {
        var ac = AssetDatabase.LoadAssetAtPath(assetLocation, typeof(ActionsCollector));
        var boo = ac as ActionsCollector;
        if (boo == null)
            throw new NullReferenceException();

        var _objectList = boo._objects;

        for(int x=0; x< _objectList.Count; x++)
        {
            var obj = GameObject.Find(_objectList[x]._Name);

            if (obj == null)
                continue;

            var val = obj.transform.GetOrAddComponent<TriggerAgent>()as TriggerAgent ;

            copyAtt<TriggerAgent>(val, _objectList[x]);
               
        }
    }


   static void copyAtt<T>(T b,TriggerObjects ob)
    {
        var chuu = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
        var properties = b.GetType().GetProperties(chuu);

        foreach (PropertyInfo source in properties)
        {
            PropertyInfo des = b.GetType().GetProperty(source.Name);
            // var val = source.GetValue(ob, null);


            var prop = ob.GetType().GetProperty(source.Name);
            if (prop == null)
                continue;
            var val = prop.GetValue(ob, null);

            if(val==null)
                {
                    Debug.Log("value is null/property does not exist");
                    continue;
                }
            des.SetValue(b,
                val,
                null);
        }
    }
}