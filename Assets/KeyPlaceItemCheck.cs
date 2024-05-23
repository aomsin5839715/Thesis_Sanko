using UnityEngine;
using UnityEngine.Playables; // Assuming you're using the PlayableDirector for the timeline

public class KeyPlaceItemCheck : MonoBehaviour
{
    public KeyItemPlace[] keyItemPlaces; // Array to hold the key item places
    public PlayableDirector timeline; // Reference to the PlayableDirector for the timeline

    private bool timelinePlayed = false; // Flag to ensure the timeline is only played once

    void Update()
    {
        if (!timelinePlayed && AreAllKeyItemsPlaced())
        {
            PlayTimeline();
        }
    }

    private bool AreAllKeyItemsPlaced()
    {
        foreach (KeyItemPlace keyItemPlace in keyItemPlaces)
        {
            if (!keyItemPlace.isCorrectKeyItem)
            {
                return false;
            }
        }
        return true;
    }

    private void PlayTimeline()
    {
        if (timeline != null && timeline.state != PlayState.Playing)
        {
            timeline.Play();
            timelinePlayed = true; // Set the flag to true to prevent replaying the timeline
        }
    }
}
