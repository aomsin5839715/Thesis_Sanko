using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointScript : MonoBehaviour
{
    public GameObject saveButton; // Reference to the save button GameObject
    public GameManager gameManager;
    public GameObject SaveCanvas;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Show the save button when the player enters the trigger area
            saveButton.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide the save button when the player exits the trigger area
            saveButton.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (saveButton.activeSelf)
            {
                // Save the game if the save button is active and the player presses the "E" key
                if (gameManager != null)
                {
                    int slotIndex = 0;
                    gameManager.SaveGame(slotIndex);
                    StartCoroutine(SaveCanvasTime());
                }
            }
        }
    }

    private IEnumerator SaveCanvasTime()
    {
            SaveCanvas.SetActive(true);
            yield return new WaitForSeconds(2);
            SaveCanvas.SetActive(false);
    }
}
