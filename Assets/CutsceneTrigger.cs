using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector cutsceneTimeline; // Reference to the PlayableDirector component with your cutscene timeline

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Play the cutscene timeline when the player enters the trigger area
            if (cutsceneTimeline != null)
            {
                cutsceneTimeline.Play();
                Destroy(this.gameObject);
            }
        }
    }
}
