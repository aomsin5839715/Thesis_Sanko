using UnityEngine;
using TMPro; // Add this line to import the TextMeshPro namespace

public class PlayerInventory : MonoBehaviour
{
    public int keysCount = 0; // Number of keys the player has
    public TextMeshProUGUI keysText; // Reference to the UI Text element

    void Start()
    {
        // Ensure the keysText reference is set
        if (keysText == null)
        {
            Debug.LogError("UI Text reference not set for keys count display.");
            enabled = false; // Disable the script if the reference is not set
            return;
        }

        UpdateKeysText(); // Update the UI Text element initially
    }

    public void AddKeys(int amount)
    {
        keysCount += amount;
        UpdateKeysText(); // Update the UI Text element after adding keys
        Debug.Log("Keys added: " + amount + ". Total keys: " + keysCount);
    }

    public void RemoveKeys(int amount)
    {
        if (keysCount >= amount)
        {
            keysCount -= amount;
            UpdateKeysText(); // Update the UI Text element after removing keys
            Debug.Log("Keys removed: " + amount + ". Total keys: " + keysCount);
        }
        else
        {
            Debug.LogWarning("Not enough keys to remove.");
        }
    }

    void UpdateKeysText()
    {
        keysText.text = keysCount.ToString(); // Update the UI Text with the current key amount
    }
}
