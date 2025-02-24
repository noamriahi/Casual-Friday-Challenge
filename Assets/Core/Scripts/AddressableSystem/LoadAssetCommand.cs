using Core;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public class LoadAssetCommand : BaseCommand
{
    private readonly string _assetKey;
    Action<GameObject> _onLoadCallback;
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
        LoadAddressableAsset();
    }
    private void LoadAddressableAsset()
    {
        Addressables.LoadAssetAsync<GameObject>(_assetKey).Completed += OnAssetLoaded;
    }
    private void OnAssetLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _onLoadCallback?.Invoke(handle.Result); 
        }
        else
        {
            Debug.LogError($"Failed to load asset: {_assetKey}");
        }
    }
}
