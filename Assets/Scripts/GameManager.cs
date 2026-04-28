using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Image[] strikeImages;
    [SerializeField] private Sprite emptyStrikeSprite;
    [SerializeField] private Sprite fullStrikeSprite;

    [Header("Game Settings")]
    [SerializeField] private float moneyPenaltyPerStrike = 10f;
    [SerializeField] private int maxStrikes = 3;

    private int strikeCount = 0;
    private float moneyCount = 0f;

    public static GameManager Instance { get; private set; }

    private int EffectiveMaxStrikes => strikeImages != null && strikeImages.Length > 0
        ? Mathf.Min(maxStrikes, strikeImages.Length)
        : maxStrikes;

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
        AddStrike(2); // Start with 1 strike for testing
        UpdateUI();
    }

    public void AddMoney(float amount)
    {
        moneyCount = Mathf.Max(0f, moneyCount + amount);
        UpdateUI();
    }

    public void SubtractMoney(float amount)
    {
        moneyCount = Mathf.Max(0f, moneyCount - amount);
        UpdateUI();
    }

    public void AddStrike(int amount = 1)
    {
        if (amount <= 0) return;

        strikeCount = Mathf.Clamp(strikeCount + amount, 0, EffectiveMaxStrikes);
        SubtractMoney(moneyPenaltyPerStrike * amount);

        if (strikeCount >= EffectiveMaxStrikes)
        {
            GameOver();
        }

        UpdateUI();
    }

    public void RemoveStrike(int amount = 1)
    {
        if (amount <= 0) return;

        strikeCount = Mathf.Clamp(strikeCount - amount, 0, EffectiveMaxStrikes);
        UpdateUI();
    }

    public void ResetStrikes()
    {
        strikeCount = 0;
        UpdateUI();
    }

    public void SetStrikeCount(int count)
    {
        strikeCount = Mathf.Clamp(count, 0, EffectiveMaxStrikes);

        if (strikeCount >= EffectiveMaxStrikes)
        {
            GameOver();
        }

        UpdateUI();
    }

    public void HandleTimerEnd(bool playerWon, float rewardAmount = 0.05f)
    {
        if (playerWon)
        {
            AddMoney(rewardAmount);
        }
        else
        {
            AddStrike();
        }
    }

    private void UpdateUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "$" + moneyCount.ToString("F2");
        }

        
        

        UpdateStrikeImages();
    }

    private void UpdateStrikeImages()
    {
        if (strikeImages == null || emptyStrikeSprite == null || fullStrikeSprite == null) return;

        for (int i = 0; i < strikeImages.Length; i++)
        {
            strikeImages[i].sprite = i < strikeCount ? fullStrikeSprite : emptyStrikeSprite;
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over! Too many strikes.");
        // TODO: Show game over UI, reload scene, or stop gameplay
    }

    public float GetMoney() => moneyCount;
    public int GetStrikes() => strikeCount;
}
