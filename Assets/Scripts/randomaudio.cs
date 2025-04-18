using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    public AudioSource[] audioSources; // Array of AudioSources to play randomly
    public float minInterval = 25f;   // Minimum interval between sounds in seconds
    public float maxInterval = 35f;   // Maximum interval between sounds in seconds

    private bool isPlaying = false;   // Is the script currently waiting between plays?

    void Start()
    {
        if (audioSources.Length == 0)
        {
            Debug.LogError("No audio sources assigned to RandomAudioPlayer.");
            return;
        }

        // Start the random playback coroutine
        StartCoroutine(PlayRandomAudio());
    }

    private System.Collections.IEnumerator PlayRandomAudio()
    {
        while (true) // Keep running indefinitely
        {
            if (!isPlaying)
            {
                isPlaying = true;

                // Randomly pick an audio source and play it
                int randomIndex = Random.Range(0, audioSources.Length);
                AudioSource selectedAudioSource = audioSources[randomIndex];

                if (selectedAudioSource != null)
                {
                    selectedAudioSource.Play();
                    Debug.Log("Playing audio: " + selectedAudioSource.clip.name);
                }
                else
                {
                    Debug.LogWarning("Selected AudioSource is null. Skipping...");
                }

                // Wait for a random interval before playing the next sound
                float delay = Random.Range(minInterval, maxInterval);
                yield return new WaitForSeconds(delay);

                isPlaying = false;
            }

            // Small yield to avoid overloading the Update loop
            yield return null;
        }
    }
}
