using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<T>(true);
            if (_instance != null && !_instance.IsInitialized())
                _instance.Initialize();
            return _instance;
        }
    }

    public bool IsInitialized()
    {
        return isInitialized;
    }

    protected bool isInitialized = false;

    public virtual void Initialize()
    {
        isInitialized = true;
    }

    protected virtual void OnDestroy()
    {
        if (this == Instance)
        {
            _instance = null;
        }
    }
}
