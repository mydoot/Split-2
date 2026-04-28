using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string sceneToLoad;

    public GameObject player;
    public float interactDistance = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryChangeScene();
        }
    }
    void TryChangeScene()
    {

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactDistance)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.Log("Player is too far away.");
        }
    }
}