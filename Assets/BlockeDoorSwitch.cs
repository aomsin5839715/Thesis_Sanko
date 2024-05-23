using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockeDoorSwitch : MonoBehaviour
{
    public GameObject ActivateObject;
    public Animator SwitchAnim;
    public GameObject UiButton;
    public bool alreadyActivated;

    private bool isPlayerNear = false;

    // Start is called before the first frame update
    void Start()
    {
        UiButton.SetActive(false); // Start with the UI button deactivated
        alreadyActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Set the boolean parameter OnActivated to true in the Animator
            SwitchAnim.SetBool("OnActivated", true);
            alreadyActivated = true;

            // Optionally, you can disable the BlockedDoor GameObject
            if (ActivateObject != null)
            {
                Animator animator = ActivateObject.GetComponent<Animator>();
                if (animator != null)
                {
                    Debug.Log("Open The Door");
                    animator.SetBool("OnActivated", true);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !alreadyActivated)
        {
            isPlayerNear = true;
            UiButton.SetActive(true);
            //Debug.Log("Near Switch");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            UiButton.SetActive(false);
        }
    }

}
