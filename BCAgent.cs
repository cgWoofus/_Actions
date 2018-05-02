using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class BCAgent : MonoBehaviour
{
    [SerializeField]
    private BlockCollection _myColl;
    private bool _eventTrigger=false;
    public BlockCollection _MyCollection { get { return _myColl; } set { _myColl = value; } }
    delegate void UIAWindowTriggerHandler(bool val);
    delegate void UIAWindowPositionUpdateHandler(Vector2 pos);
    event UIAWindowPositionUpdateHandler UIAEventWindowUpdate;
    event UIAWindowTriggerHandler UIAEventTrigger;
    private void Awake()
    {
        GetBlockCollection();

        if (_myColl == null)
            throw new NullReferenceException(); 

        //if trigger add menuoption
        if (_myColl._IsInteractable)
        {
            var uiaManager = GlobalToolBox.Instance.GetOrAddComponent<UIAGUIRoot>();
            var foo = this.transform.GetOrAddComponent<UIARightClickTriggerOption>();
            foo.Construct();
            foo.UpdateWindow = uiaManager.UpdateWindow;
        }

        //if movable add uiamovable

        //put other flag checks here

       // GlobalToolBox.Instance.GetOrAddComponent<UIInternalFeed>();
    }

    void GetBlockCollection()
    {
        //  throw new NotImplementedException();
        var _trimmedChar = this.gameObject.name.Split('_');

        if (_trimmedChar.Length < 4)
            throw new System.IndexOutOfRangeException();

        var prefabPath = string.Format("{0}_{1}_{2}", _trimmedChar[1], _trimmedChar[2], _trimmedChar[3]);
        var _blockAsset = PrefabRelated.LoadAsset<BlockCollection>(string.Format("prefab/_setPointers/{0}",prefabPath));
        _myColl = _blockAsset as BlockCollection;
        


    }

    void IsInteractable()
    {
        // if i have a trigger
    }


    void OnPointerClickDelegate(PointerEventData data)
    {
        // _trigger = true;
        _eventTrigger = !_eventTrigger;
        if (UIAEventTrigger != null)
            UIAEventTrigger(_eventTrigger);

        if (UIAEventWindowUpdate != null)
            UIAEventWindowUpdate(data.position);
    }

    private void OnDisable()
    {
        UIAEventTrigger = null;
        UIAEventWindowUpdate = null;

    }
}
