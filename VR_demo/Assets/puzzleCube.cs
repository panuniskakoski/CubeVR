using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class puzzleCube : MonoBehaviour
{
    // Helper variables
    float timer = 0.0f;
    int seconds;

    public bool rightAnswer = false;
    public bool wrongAnswer = false;
    public bool answerSubmitted = false;

    public string answerCube;
    public GameObject[] cubes;

    public GameObject[] environment;
    public bool playerDropped = false;

    public GameObject[] particles;

    // UI elements
    public SpriteRenderer whiteScreen;
    public Text qualifiedText;
    public Text disqualifiedText;
    public Text credits;

    public AudioSource audioManager;
    public AudioClip right;
    public AudioClip wrong;

    // Intro
    public Text introText;
    float introTimer = 0.0f;
    int introSeconds;
    public bool introOn;

    // Start is called before the first frame update
    void Start()
    {
        whiteScreen = GameObject.Find("whiteScreen").GetComponent<SpriteRenderer>();
        qualifiedText = GameObject.Find("Qualified").GetComponent<Text>();
        disqualifiedText = GameObject.Find("Disqualified").GetComponent<Text>();
        credits = GameObject.Find("Credits").GetComponent<Text>();

        audioManager = GameObject.Find("AltarPlace").GetComponent<AudioSource>();

        introText = GameObject.Find("IntroText").GetComponent<Text>();
        introOn = true;

        environment = GameObject.FindGameObjectsWithTag("disappears");

        cubes = GameObject.FindGameObjectsWithTag("cube");
        particles = GameObject.FindGameObjectsWithTag("particles");
        StopParticleEmission();
    }

    // Update is called once per frame
    void Update()
    {
        if (introOn) Intro();

        // Timer starts counting after answer is submitted
        // Ending events are timed.
        if (answerSubmitted) Timer();

        // You can always quit with "escape" key
        if (Input.GetButtonDown("Cancel")) Application.Quit();
        // You can restart the game with "N" key
        if (Input.GetButtonDown("Restart")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Handles the puzzle outcome
    void OnTriggerEnter(Collider other)
    {
        // Won't need to do any of this if answer is submitted.
        if (answerSubmitted) return;

        // If the player places the right cube
        if (other.tag == "cube" && other.name == "6")
        {
            answerSubmitted = true;
            rightAnswer = true;
            answerCube = other.name;
            DestroyCubes();
            // start particles
            StartParticleEmission();
            // play sound once
            audioManager.PlayOneShot(right, 0.65f);


        }
        // If the player places a wrong cube
        else if ((other.tag == "cube" || other.tag == "miscCube") && !(other.name == "6"))
        {
            answerSubmitted = true;
            wrongAnswer = true;
            answerCube = other.name;
            DestroyCubes();
            // play sound once
            audioManager.PlayOneShot(wrong, 0.8f);
        }
    }

    // Handles the timer, which plays out timed events
    void Timer()
    {
        timer += Time.deltaTime;
        seconds = (int)timer % 60;

        // If answer was correct
        if (rightAnswer)
        {
            // After 12 seconds the screen goes white
            if (seconds > 12 && seconds < 15)
            {
                whiteScreen.color += new Color(0, 0, 0, 0.01f);
            }

            // After 15 seconds shows "Qualafied" text
            if (seconds > 15 && seconds < 18)
            {
                qualifiedText.color += new Color(0, 0, 0, 0.01f);
            }

            // After 18 seconds fade out "Qualafied" text
            if (seconds > 18 && seconds < 21)
            {
                qualifiedText.color -= new Color(0, 0, 0, 0.04f);
            }

            // After 18 seconds shows "Credits" text
            if (seconds > 21 && seconds < 24)
            {
                credits.color += new Color(0, 0, 0, 0.01f);
            }
        }

        // If answer was incorrect
        if (wrongAnswer)
        {
            // After 7 seconds the floor disappears
            if (seconds > 5 && !playerDropped)
            {
                playerDropped = true;
                DropPlayer();
            }

            // After 7 seconds shows "Disqualafied" text
            if (seconds > 10 && seconds < 13)
            {
                disqualifiedText.color += new Color(0, 0, 0, 0.05f);
            }
        }
    }

    // Handles the intro text appearance
    void Intro()
    {
        introTimer += Time.deltaTime;
        introSeconds = (int)introTimer % 60;

        // Fade out the text
        if (introSeconds > 10 && introSeconds < 15) introText.color -= new Color(0, 0, 0, 0.04f);

        if (introSeconds > 15) introOn = false;
    }

    // Stops particles at the start of the game
    void StopParticleEmission()
    {
        foreach (GameObject child in particles)
        {
            child.GetComponent<ParticleSystem>().Stop();
        }
    }

    // Starts emitting particles
    void StartParticleEmission()
    {
        foreach (GameObject child in particles)
        {
            child.GetComponent<ParticleSystem>().Play();
        }
    }

    // Destroys floor and other objects
    void DropPlayer()
    {
        foreach (GameObject child in environment)
        {
            Destroy(child);
        }
    }

    // Destroys other answer cubes
    void DestroyCubes()
    {
        foreach (GameObject child in cubes)
        {
            if (child.name != answerCube) Destroy(child);
        }
    }
}
