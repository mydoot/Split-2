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
    //[SerializeField] private float moneyPenaltyPerStrike = 10f;
    [SerializeField] private int maxStrikes = 3;

    private int strikeCount = 0;
    [SerializeField] private float moneyCount = -10.03f;

    [Header("Audio")]
    [SerializeField] private AudioSource strikeSound1;
    [SerializeField] private AudioSource strikeSound2;
    [SerializeField] private AudioSource strikeSound3;

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
        UpdateUI();
    }

    public void AddMoney(float amount)
    {
        moneyCount = moneyCount + amount;
        UpdateUI();
    }

    public void SubtractMoney(float amount)
    {
        moneyCount = moneyCount - amount;
        UpdateUI();
    }

    public void AddStrike(int amount = 1)
    {
        if (amount <= 0) return;

        strikeCount = Mathf.Clamp(strikeCount + amount, 0, EffectiveMaxStrikes);
        //SubtractMoney(moneyPenaltyPerStrike * amount);

        if (strikeCount == 1)
        {
            Debug.Log("First strike! Be careful.");
            strikeSound1.Play();
        }
        else if (strikeCount == 2)
        {
            strikeSound2.Play();
        }
        else if (strikeCount >= 3)
        {
            strikeSound3.Play();
        }

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
