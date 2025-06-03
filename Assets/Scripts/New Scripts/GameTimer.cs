using System;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private float startTimeSeconds;

    private float currentTime;
    private bool isRunning = true;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        startTimeSeconds = GameManager.Instance.timeLimit;
    }

    void Start()
    {
        currentTime = startTimeSeconds;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isRunning = false;
            UpdateTimerDisplay();
            UIManager.ShowLosePanel();
            return;
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = $"{minutes} : {seconds:00}";
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }

    public void ResetTimer(float timeInSeconds)
    {
        currentTime = timeInSeconds;
        isRunning = true;
        UpdateTimerDisplay();
    }
}