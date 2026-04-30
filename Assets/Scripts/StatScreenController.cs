using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatScreenController : MonoBehaviour
{
    public Timerhand timerhand;

    public GameObject statPanel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI moneyText;

    public string sceneToLoad = "3D Scene";
    public float statScreenDuration = 5f;

    public float moneyReward = 0.01f;

    private bool hasShownStats = false;
    private float elapsedTime = 0f;

    void Start()
    {
        if (statPanel != null)
        {
            statPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (hasShownStats || timerhand == null)
            return;

        elapsedTime += Time.deltaTime;

        if (timerhand.timerText != null && timerhand.timerText.text.Trim() == "0:00")
        {
            ShowStatsAndReturn();
        }

        if (elapsedTime >= timerhand.countdownFrom)
        {
            ShowStatsAndReturn();
        }
    }

    public void ShowStatsAndReturn()
    {
        if (hasShownStats) return;

        hasShownStats = true;
        StartCoroutine(StatSequence());
    }

    private IEnumerator StatSequence()
    {
        bool won = timerhand.cardGameResult;
        float moneyMade = won ? moneyReward : 0f;

        if (statPanel != null)
        {
            statPanel.SetActive(true);
        }

        resultText.text = won ? "Success!" : "Strike!";
        timeText.text = "Time Taken: " + elapsedTime.ToString("F1") + " seconds";
        moneyText.text = "Money Made: $" + moneyMade.ToString("F2");

        yield return new WaitForSeconds(statScreenDuration);

        SceneManager.LoadScene(sceneToLoad);
    }
}