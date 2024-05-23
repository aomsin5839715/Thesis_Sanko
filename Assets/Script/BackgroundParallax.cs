using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public float sensitivity = 0.1f; // Adjust this value to control the sensitivity of the movement
    private Vector3 lastMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;
            Vector3 newPosition = transform.position + deltaMousePosition * sensitivity;
            transform.position = newPosition;
            lastMousePosition = Input.mousePosition;
        }
    }
}
