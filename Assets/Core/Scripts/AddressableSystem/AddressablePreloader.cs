using Cysharp.Threading.Tasks;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Core.Addressable
{
    public class AddressablePreloader : MonoBehaviour
    {
        [SerializeField] private Slider _loadingSlider;
        [SerializeField] private TMP_Text _loadingText;
        private string _labelToPreload = "PreloadAssets";

        private void Start()
        {
            StartCoroutine(PreloadAssets());
        }

        private IEnumerator PreloadAssets()
        {
            AsyncOperationHandle<long> getSizeHandle = Addressables.GetDownloadSizeAsync(_labelToPreload);
            yield return getSizeHandle;

            if (getSizeHandle.Status == AsyncOperationStatus.Succeeded)
            {
                long totalSize = getSizeHandle.Result;
                Addressables.Release(getSizeHandle); // Release handle to avoid memory leak

                if (totalSize > 0) // Only download if there's something new
                {
                    Debug.Log($"Total Download Size: {totalSize / (1024f * 1024f):F2} MB");

                    AsyncOperationHandle downloadHandle = Addressables.DownloadDependenciesAsync(_labelToPreload);

                    while (!downloadHandle.IsDone)
                    {
                        float progress = downloadHandle.PercentComplete;
                        _loadingSlider.value = progress;
                        _loadingText.text = $"Loading... {(progress * 100):F0}%";
                        yield return null;
                    }

                    if (downloadHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        Addressables.Release(downloadHandle); // Release handle
                        OnFinishLoading();
                    }
                    else
                    {
                        _loadingText.text = "Download Failed!";
                    }
                }
                else
                {
                    OnFinishLoading();
                }
            }
            else
            {
                _loadingText.text = "Error Fetching Data!";
            }
        }

        void OnFinishLoading()
        {
            LoadMenuScene().Forget();
        }
        async UniTaskVoid LoadMenuScene()
        {
            await Fader.Instance.FadeIn();
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("MainMenu");
            await loadOperation.ToUniTask();
            await Fader.Instance.FadeOut();
        }
    }
}
