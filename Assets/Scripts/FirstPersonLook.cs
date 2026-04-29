using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonLook : MonoBehaviour
{
    [Header("References")]
    public Transform playerCamera;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 10f;
    public float smoothSpeed = 12f;

    private float xRotation = 0f;

    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 targetMouseDelta = Mouse.current.delta.ReadValue();

        currentMouseDelta = Vector2.SmoothDamp(
            currentMouseDelta,
            targetMouseDelta,
            ref currentMouseDeltaVelocity,
            1f / smoothSpeed
        );

        float mouseX = currentMouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = currentMouseDelta.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}