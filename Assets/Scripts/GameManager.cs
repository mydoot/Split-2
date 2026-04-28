using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int strikeCount = 0;
    private float moneyCount = 0.0f;

    public static GameManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI strikeText;
    [SerializeField] private TextMeshProUGUI timerText; // Reference to the timer text
    [SerializeField] private Image[] strikeImages; // Array of images for visual strike indicators
    [SerializeField] private Sprite emptyStrikeSprite; // Sprite for empty strike (e.g., empty X)
    [SerializeField] private Sprite fullStrikeSprite; // Sprite for full strike (e.g., filled X)

    [Header("Game Settings")]
    [SerializeField] private float moneyPenaltyPerStrike = 10.0f; // Money lost per strike
    [SerializeField] private int maxStrikes = 3; // Maximum strikes before game over

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if timer has ended (assuming timer text shows "00:00" or similar when done)
        if (timerText != null && timerText.text == "00:00")
        {
            HandleTimerEnd(false, 0f); // Default: loss with no reward
            timerText.text = ""; // Reset or something to prevent repeated calls
        }
    }

    public void AddMoney(float amount)
    {
        moneyCount += amount;
        UpdateUI();
    }

    public void SubtractMoney(float amount)
    {
        moneyCount -= amount;
        if (moneyCount < 0) moneyCount = 0;
        UpdateUI();
    }

    public void AddStrike()
    {
        strikeCount++;
        SubtractMoney(moneyPenaltyPerStrike);
        if (strikeCount >= maxStrikes)
        {
            GameOver();
        }
        UpdateUI();
    }

    public void MinusStrikes()
    {
        strikeCount -= 1;
        if (strikeCount < 0) strikeCount = 0;
        UpdateUI();
    }

    public void HandleTimerEnd(bool playerWon, float rewardAmount = 0.05f)
    {   
        if (playerWon)
        {
            AddMoney(rewardAmount);
        } else
        {
            AddStrike();
        }
    }

    private void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = "$" + moneyCount.ToString("F2");

        if (strikeText != null)
            strikeText.text = "Strikes: " + strikeCount + "/" + maxStrikes;

        // Update strike images
        if (strikeImages != null && emptyStrikeSprite != null && fullStrikeSprite != null)
        {
            for (int i = 0; i < strikeImages.Length; i++)
            {
                if (i < strikeCount)
                {
                    strikeImages[i].sprite = fullStrikeSprite;
                }
                else
                {
                    strikeImages[i].sprite = emptyStrikeSprite;
                }
            }
        }
    }

    private void GameOver()
    {
        // Implement game over logic here
        Debug.Log("Game Over! Too many strikes.");
        // Perhaps load a game over scene or show a panel
    }

    // Public getters
    public float GetMoney() { return moneyCount; }
    public int GetStrikes() { return strikeCount; }
}
