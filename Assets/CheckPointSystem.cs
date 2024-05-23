using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Save the player's position
            
            SavePlayerPosition(other.transform.position);
        }
    }

    private void SavePlayerPosition(Vector3 position)
    {
        // Save the player's position using PlayerPrefs or a save/load manager
        // For example, using PlayerPrefs:
        PlayerPrefs.SetFloat("PlayerPosX", position.x);
        PlayerPrefs.SetFloat("PlayerPosY", position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", position.z);

        Debug.Log("Player position saved: " + position);
    }
}
