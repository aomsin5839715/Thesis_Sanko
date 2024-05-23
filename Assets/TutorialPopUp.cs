using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialPopUp : MonoBehaviour
{
    public Canvas tutorialCanvas;
    public Image tutorialImage;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    public Sprite newImage;
    public string newTitle;
    public string newDescription;

    public GameObject player;
    public PlayerController playerController;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Show canvas
            tutorialCanvas.enabled = true;

            // Set DashActivated to true in player controller script
            if (playerController != null)
            {
                playerController.DashActivated = true;
            }

            // Update canvas content
            SetTutorialContent();

            // Pause the game
            Time.timeScale = 0f;

            // Disable player controller
            if (playerController != null)
            {
                playerController.enabled = false;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Close canvas
            tutorialCanvas.enabled = false;

            // Resume the game
            Time.timeScale = 1f;

            // Re-enable player controller
            if (playerController != null)
            {
                playerController.enabled = true;
            }
            
        }
    }

    void SetTutorialContent()
    {
        // Update the canvas content with newImage, newTitle, and newDescription
        tutorialImage.sprite = newImage;
        titleText.text = newTitle;
        descriptionText.text = newDescription;
    }

}
