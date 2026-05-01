using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentUI : MonoBehaviour
{
    private static PersistentUI instance;

    [SerializeField] private string sceneToShowIn = "3D Scene";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateVisibility(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateVisibility(scene.name);
    }

    private void UpdateVisibility(string sceneName)
    {
        gameObject.SetActive(sceneName == sceneToShowIn);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}