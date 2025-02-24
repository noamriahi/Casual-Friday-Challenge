using System;
using UnityEngine;
using UnityEngine.UI;
namespace Core.Popups
{
    public abstract class Popup : MonoBehaviour
    {
        [Header("Popup")]
        [SerializeField] Button _closeButton;
        public Action OnDestroyPopup;
        private void Start()
        {
            _closeButton.onClick.AddListener(ClosePopup);
        }
        public abstract void InitPopup();
        protected virtual void ClosePopup()
        {
            _closeButton.onClick.RemoveListener(ClosePopup);
            Destroy(gameObject);
        }
        protected virtual void OnDestroy()
        {
            OnDestroyPopup?.Invoke();
        }
    }

}
