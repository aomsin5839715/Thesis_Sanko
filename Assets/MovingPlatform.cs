using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float minSpeed = 2f;
    public float maxSpeed = 5f;



    private Rigidbody2D rb;
    private float currentSpeed;
    private bool movingToEnd = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        Vector2 currentTarget = movingToEnd ? endPoint.position : startPoint.position;
        Vector2 currentPosition = rb.position;
        float distanceToTarget = Vector2.Distance(currentPosition, currentTarget);

        if (distanceToTarget <= 0.01f)
        {
            movingToEnd = !movingToEnd;
            currentSpeed = Random.Range(minSpeed, maxSpeed);
        }

        Vector2 moveDirection = (currentTarget - currentPosition).normalized;
        Vector2 newPosition = currentPosition + (moveDirection * currentSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startPoint.position, endPoint.position);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {

            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {

            }
        }
    }

}
