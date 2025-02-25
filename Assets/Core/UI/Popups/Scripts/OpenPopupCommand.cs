using Core.Addressable;
using System;
using UnityEngine;
namespace Core.Popups
{
    /// <summary>
    /// Load asset and then take the popup script to open it using the popupManager.
    /// </summary>
    public enum PopupType { StartGame, EndGame, Settings }
    public class OpenPopupCommand : BaseCommand
    {
        private PopupType _popupType;
        public OpenPopupCommand(PopupType popupType)
        {
            _popupType = popupType;
        }
        protected override void InternalExecute()
        {
            string path = GetPopupPath(_popupType);
            new LoadAssetCommand(path)
                .WithLoadCallback(OnLoadAsset)
                .Execute();
        }
        private void OnLoadAsset(GameObject asset)
        {
            var popup = asset.GetComponent<Popup>();
            if (popup != null)
            {
                PopupManager.Instance.OpenPopup(popup);
            }
        }
        private string GetPopupPath(PopupType type)
        {
            return type switch
            {
                PopupType.StartGame => "Popups/StartGamePopup",
                PopupType.EndGame => "Popups/EndGamePopup",
                PopupType.Settings => "Popups/SettingsPopup",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
