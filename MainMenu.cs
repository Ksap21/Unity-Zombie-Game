using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    public GameObject[] menuItems; // Assign your menu buttons or texts in the Inspector
    public AudioSource navigateSound; // Assign a navigation sound effect in the Inspector
    public AudioSource selectSound;   // Assign a select sound effect in the Inspector
    private int selectedIndex = 0;
    public static bool gameStarted = false;

    [SerializeField] private Slider volumeSlider; // Volume slider, assign in the Inspector

    void Start()
    {
        UpdateMenuSelection();

    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            navigateSound?.Play();
            selectedIndex--;
            if (selectedIndex < 0)
                selectedIndex = menuItems.Length - 1; // Wrap to the last menu item
            UpdateMenuSelection();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            navigateSound?.Play();
            selectedIndex++;
            if (selectedIndex >= menuItems.Length)
                selectedIndex = 0; // Wrap to the first menu item
            UpdateMenuSelection();
        }
        else if (Input.GetKeyDown(KeyCode.Return)) // Enter key
        {
            selectSound?.Play();
            ExecuteSelection();
        }
    }

    void UpdateMenuSelection()
    {
        for (int i = 0; i < menuItems.Length; i++)
        {
            Transform itemTransform = menuItems[i].transform;

            if (i == selectedIndex)
            {
                // Highlight with color and scaling
                Renderer renderer = menuItems[i].GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.yellow;
                }
                itemTransform.localScale = Vector3.one * 1.2f; // Scale up the selected item
            }
            else
            {
                // Reset color and scaling
                Renderer renderer = menuItems[i].GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.white;
                }
                itemTransform.localScale = Vector3.one; // Default scale
            }
        }
    }

    void ExecuteSelection()
    {
        switch (selectedIndex)
        {
            case 0: // Start Game
                StartGame();
                break;
            case 1: // Settings
                Debug.Log("Settings menu...");
                // Implement settings functionality here
                break;
            case 2: // Quit
                QuitGame();
                break;
            default:
                Debug.LogError("Invalid menu selection");
                break;
        }
    }

    public void StartGame()
    {
        Debug.Log("Loading scene: " + SceneManager.GetActiveScene().name);
        gameStarted = true;
        
        SceneManager.LoadScene("GameScene"); // Replace with your scene index or name
        Debug.Log("Loading scene: " + SceneManager.GetActiveScene().name);

    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in the editor
    #else
        Application.Quit(); // Only works in a standalone build
    #endif
    }


    public void Awake()
    {
        // Check if the slider is assigned; find it dynamically if not
        if (volumeSlider == null)
        {
            volumeSlider = FindObjectOfType<Slider>();
        }

        if (volumeSlider != null)
        {
            // Load saved volume setting
            if (PlayerPrefs.HasKey("Volume"))
            {
                float savedVolume = PlayerPrefs.GetFloat("Volume");
                SetVolume(savedVolume);
                volumeSlider.value = savedVolume;
            }
            else
            {
                // Set default volume if no saved setting exists
                SetVolume(1.0f); // Default to full volume
                volumeSlider.value = 1.0f;
            }
        }
        else
        {
            Debug.LogWarning("VolumeSlider reference not assigned but seems to work. Ensure proper setup if issues arise.");
        }
    }


    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
