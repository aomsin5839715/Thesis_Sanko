using UnityEngine;

public class SaveButton : MonoBehaviour
{
    public GameManager gameManager;
    public int id;

    public void OnSaveButtonClick()
    {
        if (gameManager != null)
        {
            gameManager.SaveGame(id);
            Debug.Log(id);
        }
        else
        {
            Debug.LogError("GameManager reference not set.");
        }
    }
}
