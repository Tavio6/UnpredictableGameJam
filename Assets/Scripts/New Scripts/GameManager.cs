using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("GameManager is null. Make sure one exists in the scene.");
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Enforce only one instance
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    [Header("Variables")] 
    public float timeLimit = 120f;

    public bool CanParryBullets = false;
}