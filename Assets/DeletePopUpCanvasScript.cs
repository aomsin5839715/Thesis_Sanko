using UnityEngine;

public class DeletePopUpCanvasScript : MonoBehaviour
{
    public GameManager gameManager;
    private int slotToDelete;

    public void SetDeleteIndex(string index)
    {
        if (int.TryParse(index, out int parsedIndex))
        {
            slotToDelete = parsedIndex;
            Debug.Log("Delete Save Slot " + parsedIndex);
        }
        else
        {
            Debug.LogError("Invalid slot index: " + index);
        }
    }

    public void ConfirmDelete()
    {
        if (gameManager != null)
        {
            gameObject.SetActive(false); // Hide the delete popup after confirming delete
            gameManager.DeleteSave(slotToDelete);
        }
        else
        {
            Debug.LogError("GameManager is not assigned.");
        }
    }

    public void CancelDelete()
    {
        gameObject.SetActive(false); // Hide the delete popup when canceling delete
    }
}
