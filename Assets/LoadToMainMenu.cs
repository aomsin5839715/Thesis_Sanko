using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadToMainMenu : MonoBehaviour
{
    public void LoadMainMenu()
    {
        // Load the main menu scene when the Quit button is clicked
        SceneManager.LoadScene("Main Menu");
        Debug.Log("LoadScene Successfull");
    }
}
