using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public CharacterMovement player;
    private bool paused;
    public GameObject pauseScreen;
    public Text pauseText;
    private string winText;
    // Keybinds
    public Keybindings keybindings;

    public int secondsToReset;
    private float countDown;

    //Audio stuff//
    //Clips
    public AudioClip backroundMusic;
    //Sources
    public AudioSource backroundMusicSource;

    private void Start()
    {
        // Initialize the countdown timer
        countDown = secondsToReset;
        // Set cursor invisible
        Cursor.visible = false;
        // Set cursor locked to the center of the game window
        Cursor.lockState = CursorLockMode.Locked;
        // Set paused to false
        paused = false;
        // Set winText
        winText = "YOU ARE WINNER!";

        //putting sound to source
        backroundMusicSource.clip = backroundMusic;
        //Play backroundmsuic
        backroundMusicSource.Play();
    }

    private void Update()
    {
        // Check if unpaused
        if (!paused)
        {
            // Substract time since last frame from the countdown
            countDown -= Time.deltaTime;
        }

        // Check if countdown has reached 0 or reset key was pressed
        if (countDown <= 0 || Input.GetKeyDown(keybindings.reset))
        {
            // Run the Reset function
            Reset();
            Debug.Log("Resetting");
        }

        // Check if pause/menu button is pressed
        if (Input.GetKeyDown(keybindings.pause))
        {
            Pause();
        }
    }

    public void ShutDown()
    {
        // Terminate program operations
        Application.Quit();
    }

    public void Reset()
    {
        // Reload the same scene
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void Win()
    {
        pauseText.text = winText;
        Pause();
    }

    public void Pause()
    {
        // Check if game is paused
        if (paused)
        {
            // Set cursor visible
            Cursor.visible = false;
            // Set cursor free
            Cursor.lockState = CursorLockMode.Locked;
            // Set paused boolean to false
            paused = false;
            // Unpause player
            player.paused = false;
            // Disable pause screen
            pauseScreen.SetActive(false);
            // Pause game time
            Time.timeScale = 1;
        }
        else
        {
            // Set cursor invisible
            Cursor.visible = true;
            // Set cursor locked to the center of the game window
            Cursor.lockState = CursorLockMode.None;
            // Set paused boolean to true
            paused = true;
            // Pause player
            player.paused = true;
            // Enable pause screen
            pauseScreen.SetActive(true);
            // Resume game time
            Time.timeScale = 0;
        }
    }
}
