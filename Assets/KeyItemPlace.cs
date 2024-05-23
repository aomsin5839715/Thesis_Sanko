using UnityEngine;

public class KeyItemPlace : MonoBehaviour
{
    public string KeyItemNameRequired;
    public bool isCorrectKeyItem;
    public GameObject objectToParent;

    void Start(){
        isCorrectKeyItem = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KeyItem"))
        {
            Grabable grabable = collision.gameObject.GetComponent<Grabable>();
            if (grabable != null && !grabable.isGrabbed)
            {
                KeyItem keyItem = collision.gameObject.GetComponent<KeyItem>();
                if (keyItem != null)
                {
                    Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.constraints = RigidbodyConstraints2D.FreezePosition; // Freeze X and Y movement
                    }

                    SetKeyItemParent(collision.gameObject);

                    if (keyItem.KeyItemName == KeyItemNameRequired)
                    {
                        isCorrectKeyItem = true;
                        Debug.Log("Correct Key Item Placed: " + KeyItemNameRequired);
                    }
                    else
                    {
                        isCorrectKeyItem = false;
                        Debug.Log("Wrong Key Item Placed: " + keyItem.KeyItemName + " (Expected: " + KeyItemNameRequired + ")");
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KeyItem"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints2D.None; // Unfreeze X and Y movement
            }

            // Reset any relevant state or feedback when a key item exits the trigger area
            isCorrectKeyItem = false;
            Debug.Log("Key Item Exited: " + collision.gameObject.name);
        }
    }

    private void SetKeyItemParent(GameObject keyItemObject)
    {
        if (objectToParent != null)
        {
            Rigidbody2D rb = keyItemObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = true; // Make the Rigidbody kinematic to prevent physics interactions
            }
            keyItemObject.transform.SetParent(objectToParent.transform);

            // Adjust the position of the key item relative to the KeyItemPlace object
            keyItemObject.transform.localPosition = Vector3.zero;
        }
    }
}
