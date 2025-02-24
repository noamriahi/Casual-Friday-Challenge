
using UnityEngine;
namespace Core
{
    public class UnitySingleton<T> : MonoBehaviour where T : UnitySingleton<T>
    {
        public static T Instance = null;

        protected virtual void Start()
        {
            if (Instance == null)
            {
                Instance = GetComponent<T>();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (this == Instance)
            {
                Instance = null;
            }
        }
    }

}
