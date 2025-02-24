using System;
using UnityEngine;
namespace Core.Popups
{
    public abstract class Popup : MonoBehaviour
    {
        public Action OnDestroyPopup;
        public abstract void InitPopup();

        protected virtual void OnDestroy()
        {
            OnDestroyPopup?.Invoke();
        }
    }

}
