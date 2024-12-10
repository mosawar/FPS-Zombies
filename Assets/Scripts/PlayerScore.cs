using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public TMP_Text timeSurvivedText; // Reference to the TMP Text for Time Survived
    public TMP_Text zombiesKilledText; // Reference to the TMP Text for Zombies Killed
    public TMP_Text zBucksText; // Reference to the TMP Text for zBucks

    private float elapsedTime = 0f; // Tracks time survived
    private int zombiesKilled = 0; // Tracks zombies killed
    private int zBucks = 0; // Tracks zBucks
    private int zBucksIncrement = 1; // Base increment for zBucks, increases over time

    void Start()
    {
        // Initialize HUD elements
        timeSurvivedText.text = "Time Survived: 00:00:00";
        zombiesKilledText.text = "Zombies Killed: 0";
        zBucksText.text = "zBucks: 0";

        // Start updating the time
        StartCoroutine(UpdateTimeSurvived());
        StartCoroutine(UpdateZBucksIncrement());
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

    IEnumerator UpdateZBucksIncrement()
    {
        while (true)
        {
            // Increase zBucks increment every 30 seconds
            yield return new WaitForSeconds(5f);
            zBucksIncrement++;
            Debug.Log($"zBucks Increment increased to: {zBucksIncrement}");
        }
    }

    public void IncrementZombiesKilled()
    {
        zombiesKilled++;
        zombiesKilledText.text = $"Zombies Killed: {zombiesKilled}";

        // Increment zBucks based on current increment value
        zBucks += zBucksIncrement;
        zBucksText.text = $"zBucks: {zBucks}";
    }
}
