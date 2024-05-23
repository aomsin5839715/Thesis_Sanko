using UnityEngine;

public class Grabable : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isGrabbed = false;
    public Transform PlayerGrabAreaPos;
    public GameObject Btn;
    public float grabDistance = 2f; // Adjust this value as needed
    public GameObject[] itemOnHold;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.E) && !isGrabbed)
        {
            // Check if the item is within grabDistance before grabbing
            if (Vector2.Distance(transform.position, PlayerGrabAreaPos.position) <= grabDistance)
            {
                Grab();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && isGrabbed)
        {
            Drop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isGrabbed)
        {
            Btn.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Btn.SetActive(false);
        }
    }

    private void Grab()
    {
        rb.isKinematic = true; // Disable physics while grabbed
        transform.parent = PlayerGrabAreaPos; // Set object's parent to PlayerGrabAreaPos
        transform.localPosition = Vector3.zero; // Reset local position to prevent offset
        Btn.SetActive(false);
        // Flip the item if needed
        Vector3 scale = transform.localScale;
        if (PlayerGrabAreaPos.localScale.x < 0) // Check the scale of the grab area
        {
            scale.x = -Mathf.Abs(scale.x); // Flip the item horizontally
        }
        else
        {
            scale.x = Mathf.Abs(scale.x); // Ensure the item is not flipped
        }
        transform.localScale = scale;

        isGrabbed = true;
    }

    private void Drop()
    {
        rb.isKinematic = false; // Enable physics on drop
        transform.parent = null; // Detach from the player's grab area
        isGrabbed = false;

        // Flip the item if needed
        Vector3 scale = transform.localScale;
        if (PlayerGrabAreaPos.localScale.x < 0) // Check the scale of the grab area
        {
            scale.x = -Mathf.Abs(scale.x); // Flip the item horizontally
        }
        else
        {
            scale.x = Mathf.Abs(scale.x); // Ensure the item is not flipped
        }
        transform.localScale = scale;
    }
}
