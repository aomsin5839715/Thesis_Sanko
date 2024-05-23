using UnityEngine;

public class DisableCanvas : MonoBehaviour
{
    public GameObject[] canvasesToDisable; // Array of canvases to disable
    public GameObject[] canvasesToEnable; // Array of canvases to enable

    void Start()
    {
        // Assuming all canvases in the arrays are active at the start
        foreach (GameObject canvas in canvasesToDisable)
        {
            canvas.SetActive(true);
        }
        foreach (GameObject canvas in canvasesToEnable)
        {
            canvas.SetActive(false);
        }
    }

    public void OnButtonClick()
    {
        // Disable all canvases in the disable array and enable all canvases in the enable array
        foreach (GameObject canvas in canvasesToDisable)
        {
            canvas.SetActive(false);
        }
        foreach (GameObject canvas in canvasesToEnable)
        {
            canvas.SetActive(true);
        }
    }

    public void ResetCanvas()
    {
        // Reset the canvases by deactivating and then activating them
        foreach (GameObject canvas in canvasesToEnable)
        {
            canvas.SetActive(false);
            canvas.SetActive(true);
        }
    }
}
