using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public TMP_Text timeSurvivedText; // Reference to the TMP Text for Time Survived
    public TMP_Text zombiesKilledText; // Reference to the TMP Text for Zombies Killed

    private float elapsedTime = 0f; // Tracks time survived
    private int zombiesKilled = 0; // Tracks zombies killed

    void Start()
    {
        // Initialize HUD elements
        timeSurvivedText.text = "Time Survived: 00:00:00";
        zombiesKilledText.text = "Zombies Killed: 0";

        // Start updating the time
        StartCoroutine(UpdateTimeSurvived());
    }

    IEnumerator UpdateTimeSurvived()
    {
        while (true)
        {
            elapsedTime += 1f;

            // Calculate hours, minutes, and seconds
            int hours = Mathf.FloorToInt(elapsedTime / 3600);
            int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            // Update the UI Text
            timeSurvivedText.text = $"Time Survived: {hours:D2}:{minutes:D2}:{seconds:D2}";

            yield return new WaitForSeconds(1f); // Wait 1 second before updating again
        }
    }

    public void IncrementZombiesKilled()
    {
        zombiesKilled++;
        zombiesKilledText.text = $"Zombies Killed: {zombiesKilled}";
    }
}
