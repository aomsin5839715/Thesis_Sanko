using UnityEngine;

public class DeleteSaveBtn : MonoBehaviour
{
    public GameManager gameManager;

    public void SetSaveIndex(int saveIndex)
    {
        if (gameManager != null)
        {
            gameManager.deletePopUpCanvas.SetActive(true);
            gameManager.deletePopUpCanvas.GetComponent<DeletePopUpCanvasScript>().SetDeleteIndex(gameManager.GetGameDataID(saveIndex));
        }
        else
        {
            Debug.LogWarning("GameManager is not assigned.");
        }
    }
}
