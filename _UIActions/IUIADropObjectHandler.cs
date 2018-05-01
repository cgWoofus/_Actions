using UnityEngine;

    interface IUIADropObjectHandler
    {
        Sprite GetImage();
        Vector2 GetBoxSize();
        Vector2 GetBoxPosition();
        void DropHandle(UnityEngine.Object obj);

    }
