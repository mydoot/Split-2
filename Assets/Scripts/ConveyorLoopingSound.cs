using UnityEngine;

public class ConveyorAudio : MonoBehaviour
{
    
    [SerializeField] AudioSource conveyorSound;

    void Start()
    {
        conveyorSound.loop = true;
        conveyorSound.Play();
    }

    void Update()
    {
        /*
        change FALSE logic to whatever logic
        when switching scenes, cutscene, etc.
        */
        if (false)
        {
            conveyorSound.loop = false;
            conveyorSound.Stop();
        }
    }
}