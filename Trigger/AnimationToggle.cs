using UnityEngine;
using System.Collections;
using TriggerNamespace;
public class AnimationToggle : MonoBehaviour , ICustomComponents
{
    Trigger _myTrigger;
    Animator _myAnimator;
    AnimationState _state;
    [SerializeField]
    string _trigger = "_trigger";

    public Trigger MyTrigger
    {
        get
        {
            return _myTrigger;
        }

        set
        {
            _myTrigger = value;
        }
    }

    public Animator MyAnimator
    {
        get
        {
            return _myAnimator;
        }

        set
        {
            _myAnimator = value;
        }
    }

    public AnimationState State
    {
        get
        {
            return _state;
        }

        set
        {
            _state = value;
        }
    }

    public string Trigger
    {
        get
        {
            return _trigger;
        }

        set
        {
            _trigger = value;
        }
    }

    private void Start()
    {
       MyAnimator = this.GetComponent<Animator>();        
    }

     public TriggerHandle GetAction()
    {
        return OnAction;
    }

    public void OnAction(bool _value)
    {
     //   MyAnimator.SetTrigger(Trigger);
          MyAnimator.SetBool(Trigger, _value);
        
    }


}
