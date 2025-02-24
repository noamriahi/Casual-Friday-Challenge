using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;

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
            newPopup.OnDestroyPopup += OpenNextPopup;
            _currentPopup = newPopup;
            
        }
    }

}
