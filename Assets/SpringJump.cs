using UnityEngine;

public class SpringJump : MonoBehaviour
{
    public float springForce = 10f;
    public Animator _Animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Apply spring force to the player's vertical velocity
                playerRb.velocity = new Vector2(playerRb.velocity.x, springForce);

                // Trigger animation if an Animator is attached
                if (_Animator != null)
                {
                    _Animator.SetTrigger("SpringJump");
                }

                Debug.Log("SpringJump triggered!");
            }
        }
    }
}
