using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
public class UIADragSwitch : UIADragFeedBackBase, IUIARootPositionInterface
{

    protected override IUIAFeedBackInterface CreateFeedBackInterface()
    {
        return new SwitchAgentTool(_targetObject);
    }
}

