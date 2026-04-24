using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody MarbleRb;
    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MarbleRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Roll();
    }

    private void Roll()
    {
        var keyboard = Keyboard.current;
        if (keyboard.wKey.isPressed)
        {
            MarbleRb.transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (keyboard.sKey.isPressed)
        {
            MarbleRb.transform.position += Vector3.back * speed * Time.deltaTime;
        }
        if (keyboard.aKey.isPressed)
        {
            MarbleRb.transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (keyboard.dKey.isPressed)
        {
            MarbleRb.transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
