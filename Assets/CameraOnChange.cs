using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

public class CameraOnChange : MonoBehaviour
{
    public CameraController cameraController; // Reference to the CameraController script
    public float newYPosition; // Specify the new Y position for the camera
    public float newMinY;
    public float newMaxY;
    public float newDistance;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                Debug.Log("Player entered CameraOnChange area");
                cameraController.originalOffset = newYPosition;
                cameraController.minYOffset = newMinY;
                cameraController.maxYOffset = newMaxY;
                cameraController.originalDistance = newDistance;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited CameraOnChange area");
            cameraController.originalOffset = 0.5f;
            cameraController.minYOffset = 0.4f;
            cameraController.maxYOffset = 0.7f;
            cameraController.originalDistance = 35f;
        }
    }
}
