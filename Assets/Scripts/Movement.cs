using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody MarbleRb;
    public float speed = 5f;
    public Camera cam;

    void Start()
    {
        MarbleRb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Roll();
    }

    private void Roll()
    {
        var keyboard = Keyboard.current;
        if (keyboard.wKey.isPressed)
        {
            MarbleRb.transform.position += cam.transform.forward * speed * Time.deltaTime;
        }
        if (keyboard.sKey.isPressed)
        {
            MarbleRb.transform.position += -cam.transform.forward * speed * Time.deltaTime;
        }
        if (keyboard.aKey.isPressed)
        {
            MarbleRb.transform.position += -cam.transform.right * speed * Time.deltaTime;
        }
        if (keyboard.dKey.isPressed)
        {
            MarbleRb.transform.position += cam.transform.right * speed * Time.deltaTime;
        }
    }
}
