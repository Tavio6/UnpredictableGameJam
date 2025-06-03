using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("References")]
    public GameObject winPanel;
    public GameObject losePanel;
    public GameTimer gameTimer;

    private void Awake()
    {
        Instance = this;
        winPanel?.SetActive(false);
        losePanel?.SetActive(false);
    }

    public static void ShowWinPanel()
    {
        if (Instance != null)
        {
            Time.timeScale = 0f;
            Instance.gameTimer.StopTimer();
            Instance.winPanel.SetActive(true);
        }
        else
            Debug.LogWarning("UIManager instance not found in scene.");
    }

    public static void ShowLosePanel()
    {
        if (Instance != null)
        {
            Time.timeScale = 0f;
            Instance.gameTimer.StopTimer();
            Instance.losePanel.SetActive(true);
        }
        else
            Debug.LogWarning("UIManager instance not found in scene.");
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}