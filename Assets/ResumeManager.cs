using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeManager : MonoBehaviour
{
    public Canvas resumeCanvas;
    public string MainMenuScene;
    public PlayerController playerController; // Add this field

    private void Start()
    {
        // Ensure the resume canvas is initially hidden
        resumeCanvas.enabled = false;

    }

    private void Update()
    {
        // Check for the ESC key press to show/hide the resume canvas
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleResumeMenu();
        }
    }

    public void ToggleResumeMenu()
    {
        // Toggle the visibility of the resume canvas
        bool isVisible = !resumeCanvas.enabled;
        resumeCanvas.enabled = isVisible;

        // Pause or resume game logic based on visibility
        Time.timeScale = isVisible ? 0f : 1f;

        // Enable or disable the PlayerController script based on visibility
        if (playerController != null)
        {
            playerController.enabled = !isVisible;
        }
    }


    public void ResumeGame()
    {
        // Hide the resume canvas and resume game logic
        bool isVisible = !resumeCanvas.enabled;
        resumeCanvas.enabled = isVisible;
        Time.timeScale = isVisible ? 0f : 1f;
        
        if (playerController != null)
        {
            playerController.enabled = !isVisible;
        }
    }

    public void QuitToMainMenu()
    {
        // Load the main menu scene when the Quit button is clicked
        SceneManager.LoadScene(MainMenuScene);
        Debug.Log("LoadScene Successfull");
    }
}
