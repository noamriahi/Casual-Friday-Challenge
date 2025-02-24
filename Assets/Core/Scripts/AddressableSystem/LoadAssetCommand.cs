using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

namespace Core.Addressable
{
    public class LoadAssetCommand : BaseCommand
    {
        private readonly string _assetKey;
        private Action<GameObject> _onLoadCallback;

        public LoadAssetCommand(string assetKey)
        {
            _assetKey = assetKey;
        }

        public LoadAssetCommand WithLoadCallback(Action<GameObject> loadCallback)
        {
            _onLoadCallback = loadCallback;
            return this;
        }

        protected override void InternalExecute()
        {
            CheckAndLoadAsset();
        }

        private void CheckAndLoadAsset()
        {
            Addressables.GetDownloadSizeAsync(_assetKey).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    long downloadSize = handle.Result;
                    Addressables.Release(handle); // Release the handle

                    if (downloadSize > 0)
                    {
                        Debug.Log($"Asset {_assetKey} needs to be downloaded. Size: {downloadSize / (1024f * 1024f):F2} MB");

                        Addressables.DownloadDependenciesAsync(_assetKey).Completed += downloadHandle =>
                        {
                            if (downloadHandle.Status == AsyncOperationStatus.Succeeded)
                            {
                                Addressables.Release(downloadHandle); // Release the download handle
                                LoadAddressableAsset(); // Now load the asset after downloading
                            }
                            else
                            {
                                Debug.LogError($"Failed to download asset {_assetKey}");
                            }
                        };
                    }
                    else
                    {
                        LoadAddressableAsset();
                    }
                }
                else
                {
                    Debug.LogError("Error getting download size for asset: " + _assetKey);
                }
            };
        }

        private void LoadAddressableAsset()
        {
            Addressables.LoadAssetAsync<GameObject>(_assetKey).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    _onLoadCallback?.Invoke(handle.Result);
                }
                else
                {
                    Debug.LogError($"Failed to load asset: {_assetKey}");
                }
            };
        }
    }
}
