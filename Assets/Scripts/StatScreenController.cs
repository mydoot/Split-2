using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatScreenController : MonoBehaviour
{
    [Header("Timer Reference")]
    public Timerhand timerhand;

    [Header("Solitaire Game Manager Reference")]
    [SerializeField] public SolitiareManager solitaireManager;

    [Header("UI References")]
    public GameObject statPanel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI moneyText;

    [Header("Scene Settings")]
    public string sceneToLoad = "3D Scene";
    public float statScreenDuration = 5f;

    [Header("Reward Settings")]
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

        if (solitaireManager.checkWin())
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
        bool won = solitaireManager.checkWin();
        float moneyMade = won ? moneyReward : 0f;

        if (GameManager.Instance != null)
        {
            Debug.Log("Strike count is now: " + GameManager.Instance.GetStrikes());
        }
        else
        {
            Debug.LogWarning("GameManager.Instance is missing, cannot update strikes or money.");
        }

        if (statPanel != null)
        {
            statPanel.SetActive(true);
        }

        resultText.text = won ? "Success!" : "Strike!";
        timeText.text = "Time Taken: " + elapsedTime.ToString("F1") + " seconds";
        moneyText.text = "Money Made: $" + moneyMade.ToString("F2");

        Debug.Log("Stat screen showing. Returning to 3D scene soon.");

        yield return new WaitForSeconds(statScreenDuration);

        SceneManager.LoadScene(sceneToLoad);
    }
}