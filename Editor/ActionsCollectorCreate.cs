using UnityEngine;
using UnityEditor;

public class ActionsCollectorCreate : ScriptableObject
{

    const string path = "Resources/data/editor/special.asset";

    [MenuItem("Tools/MyTool/Create ActionsCollector")]
    static void DoIt()
    {
        var collector = new ActionsCollector();
        //ScriptableObjectUtility.CreateAsset<ActionsCollector>(collector, path);
    }
}