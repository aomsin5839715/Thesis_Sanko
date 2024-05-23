using UnityEngine;

public class RuneKeyDoor : MonoBehaviour
{
    public int KeyRequired;
    public Animator Animation;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                if (playerInventory.keysCount >= KeyRequired)
                {
                    // Player has enough keys to open the door
                    OpenDoor();
                }
                else
                {
                    // Player does not have enough keys
                    Debug.Log("Not enough keys to open the door.");
                }
            }
        }
    }

    void OpenDoor()
    {
        Animation.SetBool("OnActivated",true);
        // Implement logic to open the door here
        Debug.Log("Door opened!");
        // You can add code here to disable the door GameObject or play an opening animation, etc.
    }
}
