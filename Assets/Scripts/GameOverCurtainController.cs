using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverCurtainController : MonoBehaviour
{
    public RectTransform topCurtain;
    public RectTransform bottomCurtain;
    public TextMeshProUGUI gameOverText;

    public GameObject hudToHide;

    public Movement playerMovement;
    public FirstPersonLook playerLook;
    public Rigidbody playerRb;

    public float delayBeforeGameOver = 5f;
    public float curtainCloseDuration = 2f;

    public float curtainOverlap = 120f;
    public float curtainExtraSize = 250f;

    private bool hasStartedGameOver = false;

    void Start()
    {
        SetupCurtains();

        Canvas gameOverCanvas = GetComponentInParent<Canvas>();

        if (gameOverCanvas != null)
        {
            gameOverCanvas.overrideSorting = true;
            gameOverCanvas.sortingOrder = 999;
        }

        if (GameManager.Instance != null && GameManager.Instance.GetStrikes() >= 3)
        {
            StartCoroutine(GameOverSequence());
        }
    }

    private void SetupCurtains()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }

        Canvas canvas = GetComponentInParent<Canvas>();

        if (canvas == null)
        {
            return;
        }

        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        float halfHeight = canvasRect.rect.height / 2f;
        float curtainHeight = halfHeight + curtainOverlap + curtainExtraSize;
        float curtainWidthExtra = curtainExtraSize;

        if (topCurtain != null)
        {
            topCurtain.anchorMin = new Vector2(0f, 1f);
            topCurtain.anchorMax = new Vector2(1f, 1f);
            topCurtain.pivot = new Vector2(0.5f, 1f);
            topCurtain.sizeDelta = new Vector2(curtainWidthExtra, curtainHeight);
            topCurtain.anchoredPosition = new Vector2(0f, curtainHeight);
        }

        if (bottomCurtain != null)
        {
            bottomCurtain.anchorMin = new Vector2(0f, 0f);
            bottomCurtain.anchorMax = new Vector2(1f, 0f);
            bottomCurtain.pivot = new Vector2(0.5f, 0f);
            bottomCurtain.sizeDelta = new Vector2(curtainWidthExtra, curtainHeight);
            bottomCurtain.anchoredPosition = new Vector2(0f, -curtainHeight);
        }
    }

    private IEnumerator GameOverSequence()
    {
        if (hasStartedGameOver) yield break;
        hasStartedGameOver = true;

        yield return new WaitForSeconds(delayBeforeGameOver);

        HideHud();

        FreezePlayer();

        yield return StartCoroutine(CloseCurtains());

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "GAME OVER";
        }
    }

    private void HideHud()
    {
        if (hudToHide == null)
        {
            return;
        }

        hudToHide.SetActive(false);
    }

    private void FreezePlayer()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (playerLook != null)
        {
            playerLook.enabled = false;
        }

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator CloseCurtains()
    {
        if (topCurtain == null || bottomCurtain == null)
        {
            yield break;
        }

        float elapsed = 0f;

        Vector2 topStart = topCurtain.anchoredPosition;
        Vector2 bottomStart = bottomCurtain.anchoredPosition;

        Vector2 topEnd = new Vector2(0f, curtainOverlap / 2f);
        Vector2 bottomEnd = new Vector2(0f, -curtainOverlap / 2f);

        while (elapsed < curtainCloseDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / curtainCloseDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            topCurtain.anchoredPosition = Vector2.Lerp(topStart, topEnd, t);
            bottomCurtain.anchoredPosition = Vector2.Lerp(bottomStart, bottomEnd, t);

            yield return null;
        }

        topCurtain.anchoredPosition = topEnd;
        bottomCurtain.anchoredPosition = bottomEnd;
    }
}