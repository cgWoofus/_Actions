﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

 public  class UIADragTrigger : UIADragFeedBackBase, IUIARootPositionInterface
{
    protected override IUIAFeedBackInterface CreateFeedBackInterface()
    {
        return new TriggerAgentTool(_targetObject);
    }
}




