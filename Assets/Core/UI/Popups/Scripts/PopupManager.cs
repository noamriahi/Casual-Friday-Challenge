using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Core.Popups
{
    public class PopupManager : UnitySingleton<PopupManager>
    {
        [SerializeField] Canvas _popupCanvas;

        Queue<Popup> _popupsQueue = new();
        Popup _currentPopup;

        public void OpenPopup(Popup popup)
        {
            _popupsQueue.Enqueue(popup);
            OpenNextPopup();
        }
        private void OpenNextPopup()
        {
            if (_currentPopup != null) return;
            if (_popupsQueue.Count == 0) return;
            var popup = _popupsQueue.Dequeue();
            var newPopup = Instantiate(popup, _popupCanvas.transform);
            newPopup.InitPopup();
            newPopup.OnDestroyPopup += ()=> 
            {
                Addressables.ReleaseInstance(newPopup.gameObject);
                newPopup.OnDestroyPopup = null;
                _currentPopup = null;
                OpenNextPopup();
            };
            _currentPopup = newPopup;
            
        }
    }

}
