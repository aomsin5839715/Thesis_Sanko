using UnityEngine;
using System.Collections;

public class OverlayTrigger : MonoBehaviour
{
    private SpriteRenderer overlaySprite; // Reference to the SpriteRenderer component
    public float fadeDuration = 1.0f; // Duration of the fade effect in seconds

    private void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        overlaySprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Start fading the overlay sprite out
            StartCoroutine(FadeOutSprite());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Optionally, start fading the overlay sprite back in
            StartCoroutine(FadeInSprite());
        }
    }

    private IEnumerator FadeOutSprite()
    {
        Color spriteColor = overlaySprite.color;
        float startAlpha = spriteColor.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, time / fadeDuration);
            spriteColor.a = alpha;
            overlaySprite.color = spriteColor;
            yield return null;
        }

        // Ensure the alpha is set to 0 at the end
        spriteColor.a = 0;
        overlaySprite.color = spriteColor;
    }

    private IEnumerator FadeInSprite()
    {
        Color spriteColor = overlaySprite.color;
        float startAlpha = spriteColor.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 1, time / fadeDuration);
            spriteColor.a = alpha;
            overlaySprite.color = spriteColor;
            yield return null;
        }

        // Ensure the alpha is set to 1 at the end
        spriteColor.a = 1;
        overlaySprite.color = spriteColor;
    }
}
