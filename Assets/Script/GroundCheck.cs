using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.2f;

    private bool isGrounded;

    public bool IsGrounded => isGrounded;

    private void Update()
    {
        // Perform ground check
        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw ground check gizmo in scene view
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
    }
}
