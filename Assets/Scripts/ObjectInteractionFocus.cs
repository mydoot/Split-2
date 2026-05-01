using UnityEngine;
using UnityEngine.InputSystem;

public class InteractTeleportBehindPlayer : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform objectToTeleport;
    public FirstPersonLook firstPersonLook;

    [Header("Interaction")]
    public float interactDistance = 3f;

    [Header("Teleport Position")]
    public float behindDistance = 1.5f;
    public float heightOffset = 1.2f;

    void Update()
    {
        float distanceToInteractable = Vector3.Distance(player.position, transform.position);

        if (distanceToInteractable <= interactDistance &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            TeleportObjectBehindPlayer();
            FocusCameraOnObject();
        }
    }

    void TeleportObjectBehindPlayer()
    {
        Vector3 flatForward = player.forward;
        flatForward.y = 0f;
        flatForward.Normalize();

        Vector3 newPosition = player.position - flatForward * behindDistance;
        newPosition.y = player.position.y + heightOffset;

        objectToTeleport.position = newPosition;
    }
    void FocusCameraOnObject()
    {
        firstPersonLook.FocusOnPosition(objectToTeleport.position);
    }
}