using UnityEngine;
using TMPro;

public class Timerhand : MonoBehaviour
{
    public TextMeshPro timerText;
    public float countdownFrom = 300f; // Set your time in seconds
    private float timeRemaining;
    private bool isRunning = true;

    void Start()
    {
        timeRemaining = countdownFrom;
    }

    void Update()
    {
        if (!isRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            isRunning = false;
            OnTimerEnd();
        }

        int minutes = (int)(timeRemaining / 60);
        int seconds = (int)(timeRemaining % 60);

        timerText.text = string.Format("{0}:{1:D2}", minutes, seconds);
    }

    void OnTimerEnd()
    {
        timerText.text = "0:00";
        Debug.Log("Timer finished!");
        // Add whatever you want to happen when time runs out here
    }
}