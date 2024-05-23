using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float screenYOffsetSpeed = 0.5f; // Adjust the speed at which m_ScreenY changes
    public float minYOffset = 0.1f; // Minimum m_ScreenY value
    public float maxYOffset = 0.9f; // Maximum m_ScreenY value

    private CinemachineFramingTransposer framingTransposer;
    public float originalOffset = 0.5f; // Default original offset
    public float originalDistance = 35f;
    public bool isPlayerNotMoving;

    private void Start()
    {
        if (virtualCamera != null)
        {
            // Get the Framing Transposer from the virtual camera
            framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

            if (framingTransposer != null)
            {
                // Store the original m_ScreenY value
                originalOffset = framingTransposer.m_ScreenY;
                originalDistance = framingTransposer.m_CameraDistance;
                framingTransposer.m_ScreenY = originalOffset;
                framingTransposer.m_CameraDistance = originalDistance;
            }
            else
            {
                Debug.LogError("Framing Transposer not found on the virtual camera.");
            }
        }
        else
        {
            Debug.LogError("Virtual camera not assigned! Assign a CinemachineVirtualCamera component to the CameraController script.");
        }
    }

    private void Update()
    {
        // Check if player is not moving
        isPlayerNotMoving = Input.GetAxisRaw("Horizontal") == 0;

        // Adjust the screen Y offset based on player input if player is not moving
        if (isPlayerNotMoving)
        {
            if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(MoveScreenY(screenYOffsetSpeed * Time.deltaTime));
            }
            else if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine(MoveScreenY(-screenYOffsetSpeed * Time.deltaTime));
            }

            // Reset to original offset if no input
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && framingTransposer != null)
            {
                framingTransposer.m_ScreenY = originalOffset;
            }
        }
        else
        {
            framingTransposer.m_ScreenY = originalOffset;
            framingTransposer.m_CameraDistance = originalDistance;
        }
    }

    public IEnumerator MoveScreenY(float offset)
    {
        yield return new WaitForSeconds(0.5f);
        
        if (framingTransposer != null)
        {
            float currentOffset = framingTransposer.m_ScreenY;
            currentOffset += offset;
            framingTransposer.m_ScreenY = Mathf.Clamp(currentOffset, minYOffset, maxYOffset);
        }
    }

    public void MoveDistance(float offset)
    {
        if (framingTransposer != null)
        {
            framingTransposer.m_CameraDistance = offset;
        }
    }

}   
