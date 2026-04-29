using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Buy : MonoBehaviour
{
    public float money = 10f;
    public float price = 5f;
    public TextMeshPro buyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buyText.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Movement movement = other.gameObject.GetComponent<Movement>();
        if (movement != null)
        {
            buyText.text = "Press E to purchase";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var keyboard = Keyboard.current;
        if (keyboard.eKey.isPressed)
        {
            Purchase();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        buyText.text = " ";
    }

    private void Purchase()
    {
        if (money >= price)
            {
                money -= price;
                Destroy(gameObject);
                Debug.Log("Item bought! Remaining money: " + money);
            }
            else
            {
                Debug.Log("Not enough money to buy the item.");
            }
    }
}