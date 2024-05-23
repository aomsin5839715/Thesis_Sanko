// RuneKey.cs
using UnityEngine;
using System;

public class RuneKey : MonoBehaviour
{
    public int keyAmount = 1; // Amount of keys this object represents
    public GameManager gameManager;
    public static string runeKeyID; // Unique ID for each RuneKey object, made static

    void Start()
    {
        runeKeyID = Guid.NewGuid().ToString();
        Debug.Log(runeKeyID);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                
                playerInventory.AddKeys(keyAmount);
                Destroy(gameObject); // Destroy the RuneKey GameObject

                // No need to pass this to SaveGame anymore
                // gameManager.SaveGame(0, this); // Assuming slotIndex is 0
            }
        }
    }
}
