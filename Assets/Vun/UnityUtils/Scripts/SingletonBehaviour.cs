using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
#if UNITY_EDITOR
            // Try to find this instance first
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();

                if (_instance != null)
                {
                    Debug.LogWarning($"{typeof(T).Name}.Instance is called before Awake");
                }
            }

            // Can't find an instance of this in the scene, so we create a temporary instance for the play session
            if (_instance == null)
            {
                Debug.LogWarning($"Can't find an instance of {typeof(T).Name}");

                var obj = new GameObject
                {
                    name = typeof(T).Name
                };

                obj.AddComponent<T>();
            }
#endif

            return _instance;
        }
    }

    protected virtual void Awake()
    {
#if UNITY_EDITOR
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning($"Singleton of type {typeof(T).Name} is being overwritten");
        }
#endif

        _instance = this as T;
#if VUN_SERVICE_LOCATOR
        ServiceLocator.Add(this);
#endif
    }

    protected virtual void OnDestroy()
    {
#if VUN_SERVICE_LOCATOR
        ServiceLocator.Remove(this);
#endif
    }
}