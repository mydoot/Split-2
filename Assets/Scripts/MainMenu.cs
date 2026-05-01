using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public class randomAudio
    {
        public AudioSource source { get; set; }
        public string name; // only here for debugging purposes
        public float timer = 0f;
        public float cooldown { get; set; }
        public float chance { get; set; }

        public void timerReset()
        {
            timer = 0f;
        }
    }
    public List<randomAudio> audios = new List<randomAudio>();

    [Header("--- AUDIO SOURCES ---")]
    [SerializeField] AudioSource ambience;
    [SerializeField] AudioSource forkliftReversing;
    [SerializeField] AudioSource myLeg;

    public void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("3D Scene");
    }

    public void StartCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Start()
    {
        ambience.loop = true;
        ambience.Play();

        AudioInit();
    }

    public void AudioInit()
    {
        audios.Add(
            new randomAudio
            {
                source = forkliftReversing,
                name = "forklift",
                cooldown = 10f,
                chance = 0.5f
            }
        );

        audios.Add(
            new randomAudio
            {
                source = myLeg,
                name = "my leg",
                cooldown = 15f,
                chance = 0.25f
            }
        );
    }

    public void Update()
    {
        foreach (randomAudio audio in audios)
        {
            audio.timer += Time.deltaTime;
            if (audio.timer >= audio.cooldown)
            {
                //Debug.Log($"{audio.name} chance...");
                audio.timerReset();
                if (Random.Range(0.0f, 1.0f) >= audio.chance)
                {
                    audio.source.Play();
                    //Debug.Log($"{audio.name}played!");
                }
            }
        }
    }
}