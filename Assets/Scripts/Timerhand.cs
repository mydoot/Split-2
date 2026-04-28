using UnityEngine;
using TMPro;
using System.Collections;

public class Timerhand : MonoBehaviour
{
    public bool cardGameResult = false; // Placeholder, set this based on actual game logic
    public TextMeshPro timerText;
    public float countdownFrom = 300f;
    private float timeRemaining;
    private bool isRunning = true;

    [SerializeField] AudioSource clockTick;
    [SerializeField] AudioSource clockFinish;
    [SerializeField] Transform Pointer;

    // Where the pointer lives off screen (set this in inspector to below/side of screen)
    [SerializeField] private Vector3 offScreenPos = new Vector3(-5.5f, -10f, 0f);
    // Where it creeps up to (slightly past the watch face)
    [SerializeField] private Vector3 watchPos = new Vector3(-5.10f, -3.3f, 0f);

    // Creep settings
    [SerializeField] private float creepDuration = 1.8f;

    // Tap settings
    [SerializeField] private float tapScaleDown = 0.80f;   // How much it squishes on tap
    [SerializeField] private float tapTiltAngle = 12f;    // Z rotation tilt on press
    [SerializeField] private float tapDownDuration = 0.5f;
    [SerializeField] private float tapUpDuration = .5f;
    [SerializeField] private float tapCount = 3f;           // How many taps before retreating

    private Vector3 pointerStartScale;
    private bool hasAnimatedThisMinute = false;
    private bool isAnimating = false;

    void Start()
    {
        timeRemaining = countdownFrom;
        pointerStartScale = Pointer.localScale;

        // Start the pointer off screen
        Pointer.localPosition = offScreenPos;

        clockTick.loop = true;
        clockTick.Play();
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

        if (seconds == 05 && !hasAnimatedThisMinute && !isAnimating)
        {
            hasAnimatedThisMinute = true;
            AnimatePointer();
        }
        else if (seconds != 0)
        {
            hasAnimatedThisMinute = false;
        }
    }

    public void OnTimerEnd()
    {
        if (cardGameResult)
        {
            // Player won, reward them
            GameManager.Instance.HandleTimerEnd(true, 0.05f); // Example: reward $0.05 for winning
        } else
        {
            // Player lost, penalize them
            GameManager.Instance.HandleTimerEnd(false); // No reward, just a strike
        }
        timerText.text = "0:00";
        Debug.Log("Timer finished!");

        clockTick.loop = false;
        clockTick.Stop();
        clockFinish.Play();
    }

    void AnimatePointer()
    {
        StartCoroutine(FullPointerSequence());
    }

    private IEnumerator FullPointerSequence()
    {
        isAnimating = true;

        // Step 1: Creep up from off screen to slightly past the watch
        yield return StartCoroutine(MoveToPos(offScreenPos, watchPos, creepDuration, easeIn: true));

        // Small pause at the watch before tapping
        yield return new WaitForSeconds(0.2f);

        // Step 2: Do a couple of taps
        for (int i = 0; i < tapCount; i++)
        {
            yield return StartCoroutine(TapDown());
            yield return StartCoroutine(TapUp());

            if (i < tapCount - 1)
                yield return new WaitForSeconds(0.1f); // brief pause between taps
        }

        // Step 3: Retreat back off screen
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(MoveToPos(watchPos, offScreenPos, creepDuration * 0.6f, easeIn: false));

        isAnimating = false;
    }

    private IEnumerator TapDown()
    {
        float elapsed = 0f;
        Vector3 startScale = Pointer.localScale;
        Vector3 targetScale = pointerStartScale * tapScaleDown;
        Quaternion startRot = Pointer.localRotation;
        Quaternion targetRot = Quaternion.Euler(0f, 0f, tapTiltAngle);

        while (elapsed < tapDownDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / tapDownDuration);
            Pointer.localScale = Vector3.Lerp(startScale, targetScale, t);
            Pointer.localRotation = Quaternion.Lerp(startRot, targetRot, t);
            yield return null;
        }
    }

    private IEnumerator TapUp()
    {
        float elapsed = 0f;
        Vector3 startScale = Pointer.localScale;
        Quaternion startRot = Pointer.localRotation;

        while (elapsed < tapUpDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / tapUpDuration);
            Pointer.localScale = Vector3.Lerp(startScale, pointerStartScale, t);
            Pointer.localRotation = Quaternion.Lerp(startRot, Quaternion.identity, t);
            yield return null;
        }

        Pointer.localScale = pointerStartScale;
        Pointer.localRotation = Quaternion.identity;
    }

    private IEnumerator MoveToPos(Vector3 from, Vector3 to, float duration, bool easeIn)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Ease in = slow start (creeping), Ease out = slow stop (retreating)
            t = easeIn ? t * t : Mathf.SmoothStep(0f, 1f, t);

            Pointer.localPosition = Vector3.Lerp(from, to, t);
            yield return null;
        }

        Pointer.localPosition = to;
    }
}