using UnityEngine;
using TriggerNamespace;

 public  class TriggerSwitchBase: MonoBehaviour
{
    [SerializeField]
    bool _triggerValue;
    event TriggerHandle GoFunctionsExecute;


    

    ITSwitch[] switchConditions;

    [SerializeField]
    int _subscribers;

    public ITSwitch[] SwitchConditions
    {
        get
        {
            return switchConditions;
        }

        set
        {
            switchConditions = value;
        }
    }

    public int GetObjectSubscribersCount()
    {
        return GoFunctionsExecute.GetInvocationList().Length;
    }

    /// <summary>
    /// Create and Attach Instance
    /// </summary>
    /// <param name="switchObject"></param>
    /// <param name="GoFunction"></param>
    public static void Construct(Object switchObject, TriggerHandle GoFunction)
    {

       var objectAsGO = switchObject as GameObject;
        if (objectAsGO != null)
            {
               var ts =ApplyAsGameObject(objectAsGO);
               ts.GoFunctionsExecute += GoFunction;
            }
    }

    static TriggerSwitchBase ApplyAsGameObject(GameObject obj)
    {
        var ts = obj.transform.GetOrAddComponent<TriggerSwitchBase>();
        //search for controllers
        ts.SwitchConditions = GetSwitchBehaviours(obj);
        return ts;

    }

    static ITSwitch[] GetSwitchBehaviours(GameObject obj)
    {
        var swits = obj.GetComponents<ITSwitch>();
        return swits;
    }

    private void Start()
    {
        //attach conditions to Go()
        if (switchConditions != null || switchConditions.Length > 0)
            AttachConditions();
    }

    public void AttachConditions()
    {
        foreach(ITSwitch condition  in switchConditions)
        {
            _subscribers++;
            condition.SetHandle(MainGO);
        }
    }

    void MainGO(bool value)
    {
        if(GoFunctionsExecute != null)
            GoFunctionsExecute(value);
    }

}

