using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f; // Movement speed
    public float patrolSpeed = 3f; // Speed during patrol
    public float chaseSpeed = 7f; // Speed when chasing the player
    public float rotationSpeed = 180f; // Rotation speed

    [Header("Detection Settings")]
    public float detectionRange = 10f; // Detection range
    public float detectionAngle = 90f; // Detection angle in degrees

    private Transform player; // Reference to the player's transform
    private Rigidbody2D rb;
    private bool isChasingPlayer = false;
    private bool isGrounded = false; // Flag to check if the enemy is grounded

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player != null)
        {
            if (IsPlayerInDetectionRange())
            {
                if (IsPlayerInDetectionAngle())
                {
                    isChasingPlayer = true;
                    ChasePlayer();
                }
                else
                {
                    isChasingPlayer = false;
                    RotateTowardsPlayer();
                    Patrol();
                }
            }
            else
            {
                isChasingPlayer = false;
                Patrol();
            }
        }
    }

    bool IsPlayerInDetectionRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        return distanceToPlayer <= detectionRange;
    }

    bool IsPlayerInDetectionAngle()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        float angle = Vector2.Angle(transform.up, directionToPlayer);
        return angle <= detectionAngle * 0.5f;
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * chaseSpeed;
    }

    void Patrol()
    {
        // Check if the enemy is grounded before changing direction
        if (isGrounded)
        {
            rb.velocity = transform.up * patrolSpeed;
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }

    void RotateTowardsPlayer()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Vector3 detectionDirection = Quaternion.AngleAxis(-detectionAngle * 0.5f, transform.forward) * transform.up;
        Vector3 start = transform.position;
        Vector3 end = start + detectionDirection * detectionRange;
        Gizmos.DrawLine(start, end);

        detectionDirection = Quaternion.AngleAxis(detectionAngle * 0.5f, transform.forward) * transform.up;
        end = start + detectionDirection * detectionRange;
        Gizmos.DrawLine(start, end);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
