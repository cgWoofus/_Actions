using UnityEngine;
using System.Collections;

public class AnimationToggle : MonoBehaviour
{
    Trigger _myTrigger;
    Animator _myAnimator;
    AnimationState _state;
    [SerializeField]
    string _trigger = "_trigger";


    private void Start()
    {
        _myTrigger = this.GetComponent<Trigger>();
        _myAnimator = this.GetComponent<Animator>();
        if(_myTrigger!=null && _myAnimator!= null)
            _myTrigger.eventHook(OnAction);
       var stat=  _myAnimator.GetCurrentAnimatorStateInfo(0);
        
    }

    void OnAction(bool _value)
    {
        _myAnimator.SetTrigger(_trigger);
    }


}
