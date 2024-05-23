using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class NewGameTimelinePlay : MonoBehaviour
{
    public Button newGameButton; // Reference to your "New Game" button
    public PlayableDirector timelineDirector; // Reference to your Timeline PlayableDirector

    void Start()
    {
        // Add a listener to the button's onClick event
        newGameButton.onClick.AddListener(StartTimeline);
    }

    void StartTimeline()
    {
        // Play the Timeline when the button is clicked
        if (timelineDirector != null)
        {
            // Disable all canvases using the CanvasManager
            timelineDirector.Play();
        }
    }

    // Clean up the listener when the script is disabled or destroyed
    void OnDisable()
    {
        newGameButton.onClick.RemoveListener(StartTimeline);
    }

    void OnDestroy()
    {
        newGameButton.onClick.RemoveListener(StartTimeline);
    }
}
