using UnityEngine;
using TMPro;

public class FullscreenToggle : MonoBehaviour
{
    public UnityEngine.UI.Button fullscreenButton; // Reference to your fullscreen toggle button
    public TMP_Text buttonText; // Reference to the TextMeshPro text component of the button

    private bool isFullscreen = false;

    void Start()
    {
        // Add a listener to the button's onClick event
        fullscreenButton.onClick.AddListener(ToggleFullscreen);

        // Update the initial text of the button
        UpdateButtonText();
    }

    public void ToggleFullscreen()
    {
        // Toggle fullscreen mode
        Screen.fullScreen = !Screen.fullScreen;

        // Update the text of the button
        isFullscreen = !isFullscreen;
        UpdateButtonText();
    }

    void UpdateButtonText()
    {
        buttonText.text = isFullscreen ? "On" : "Off";
    }

    // Clean up the listener when the script is disabled or destroyed
    void OnDisable()
    {
        fullscreenButton.onClick.RemoveListener(ToggleFullscreen);
    }

    void OnDestroy()
    {
        fullscreenButton.onClick.RemoveListener(ToggleFullscreen);
    }
}
