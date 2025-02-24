using Core;
using Core.Addressable;
using UnityEngine;
namespace Core.Popups
{
    /// <summary>
    /// Load asset and then take the popup script to open it using the popupManager.
    /// </summary>
    public class OpenPopupCommand : BaseCommand
    {
        private string _addressName;
        public OpenPopupCommand(string addressName) 
        {
            _addressName = addressName;
        }
        protected override void InternalExecute()
        {
            new LoadAssetCommand(_addressName)
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
    }
}
