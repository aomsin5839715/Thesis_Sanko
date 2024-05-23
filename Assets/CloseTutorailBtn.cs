using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTutorailBtn : MonoBehaviour
{
    public Canvas tutorialCanvas;
    public PlayerController playerController;

    public void CloseTutorialBtn()
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
