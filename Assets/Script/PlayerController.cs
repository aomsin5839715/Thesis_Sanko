using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 8f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public float slopeFriction = 0.1f; // Adjust slope friction as needed

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public Collider2D playerCollider;
    public TrailRenderer trail;
    public float smoothness = 5f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 14f;

    [Header("Boolean")]
    public bool isGrounded;
    private bool isOnSlope;
    private float slopeAngle;
    public bool isFacingRight = true;
    public bool hasDashed;
    public bool isDashing;
    public bool DashActivated;
    public bool canControlled;
    public bool isDead;
    
    private GameManager gameManager; // Reference to the GameManager
    private Transform grabItemArea;


    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        gameManager = FindObjectOfType<GameManager>();

        isDead = false;
        grabItemArea = transform.Find("GrabItemArea");
    }

    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        isGrounded = playerCollider.IsTouchingLayers(groundLayer);

        animator.SetBool("isGrounded", isGrounded);

        float move = Input.GetAxisRaw("Horizontal");
        Flip(move);

        if (Input.GetMouseButtonDown(1) && hasDashed && DashActivated)
        {
            Dash(x, y);
        }

    }

    void FixedUpdate()
    {

        if (canControlled == false)
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // Freeze velocity
            return;
        }

        float move = Input.GetAxisRaw("Horizontal");
        var zeroHealth = GetComponent<HealthBar>();
        

        if (zeroHealth.health <= 0)
        {
            StartCoroutine(WaitForLoadGameOnDead());
        }

        if (!isOnSlope || (isOnSlope && Mathf.Abs(move) > 0.1f))
        {
            Walk(move);
        }

        if (move != 0f)
        {
            animator.SetFloat("isRunning", 1f);
        }
        else
        {
            animator.SetFloat("isRunning", 0f);
        }

        if (isGrounded)
        {
            hasDashed = true;
            //NormalizeSlope(); // Call NormalizeSlope method
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    public void Walk(float move)
    {
        Vector2 targetVelocity = new Vector2(move * speed, rb.velocity.y);
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.deltaTime * smoothness);
    }

    void NormalizeSlope () {
        // Attempt vertical normalization
        if (isGrounded) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.5f, groundLayer);
            
            if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f) {
                // Apply the opposite force against the slope force 
                // You will need to provide your own slopeFriction to stabalize movement
                rb.velocity = new Vector2(rb.velocity.x - (hit.normal.x * slopeFriction), 0);

                //Move Player up or down to compensate for the slope below them
                Vector3 pos = transform.position;
                pos.y += -hit.normal.x * Mathf.Abs(rb.velocity.x) * Time.fixedDeltaTime * (rb.velocity.x - hit.normal.x > 0 ? 1 : -1);
                transform.position = pos;
            }
        }
    }

    private void Dash(float x, float y)
    {
        hasDashed = false;
        trail.emitting = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y).normalized;

        rb.velocity += dir * dashSpeed;
        
        StartCoroutine(DashWait());
    }

    private IEnumerator DashWait()
    {
        isDashing = true;
        
        yield return new WaitForSeconds(0.2f);

        isDashing = false;
        rb.gravityScale = 3;

        trail.emitting = false;
    }

    private IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (isGrounded)
            hasDashed = false;
    }

    private void Jump()
    {
        // Ensure horizontal velocity is maintained during jump
        rb.velocity = new Vector2(rb.velocity.x, 0);

        // Add vertical jump force
        rb.velocity += Vector2.up * jumpForce;

        // Update animator parameters
        animator.SetBool("isJumping", true);
        animator.SetBool("isGrounded", false);
    }

    private void Flip(float move)
    {
        if (move > 0 && !isFacingRight || move < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            // Slow down the player's speed temporarily
            speed = 5f;

            // Start a coroutine to restore the speed after a delay
            StartCoroutine(RestoreSpeed());
        }
    }

    private IEnumerator RestoreSpeed()
    {
        // Wait for a short duration
        yield return new WaitForSeconds(0.1f);

        // Restore the original speed
        speed = 8f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Add relevant logic for collision with ground
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Add relevant logic for exiting ground collision
        }
    }

    private IEnumerator WaitForLoadGameOnDead()
    {
        isDead = true;
        canControlled = false;
        if (isDead)
        {
            rb.velocity = Vector2.zero; // Freeze velocity
            animator.SetBool("isDead", true);

            var deadScreenTimeLine = GameObject.Find("DeadScreenTimeLine").GetComponent<PlayableDirector>();
            if (deadScreenTimeLine != null)
            {
                deadScreenTimeLine.Play(); // Play the Timeline
            }
            else
            {
                Debug.LogWarning("DeadScreenTimeline not assigned or not found.");
            }
        }

        yield return new WaitForSeconds(3f); // Adjust this duration to match your Timeline's length
        Dead();
    }

    public void Dead()
    {
        if (gameManager != null)
        {
            gameManager.LoadGameData(0); // Load the last saved game from slot 0
            Debug.Log("Dead: Reloaded the last saved file.");
        }
        else
        {
            Debug.LogError("GameManager reference is not set!");
        }
    }

    public void SetCanBeControlled(bool canBeControlled)
    {
        canControlled = canBeControlled;
    }

}

