using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public GameObject player;
    public float far = 4, near = 2;
    public float breakDistance = 1;  // Distance at which the light "breaks"
    Vector3 point;    
    float time = 0;

    // Add reference to AudioSource and AudioClip
    public AudioClip flickerNoise;
    public AudioClip breakNoise;  // New sound for the light breaking
    private AudioSource audioSource;

    private bool lightBroken = false;  // Flag to track if the light is broken

    void Start()
    {
        point = transform.GetChild(0).position;

        // Get or add the AudioSource component
        audioSource = GetComponent<AudioSource>();
        
        // If AudioSource is missing, add it dynamically
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        
        // Assign the custom AudioClip to the AudioSource
        if (flickerNoise != null)
            audioSource.clip = flickerNoise;
            
        // Configure the AudioSource for 3D sound
        audioSource.spatialBlend = 1.0f; // Make it 3D sound
        audioSource.dopplerLevel = 0.0f; // Optional: Disable Doppler effect (can be set to 1 for realistic movement effects)
        audioSource.rolloffMode = AudioRolloffMode.Linear; // You can experiment with Logarithmic or Custom if needed
        audioSource.minDistance = 1f; // At this distance, the sound will be at full volume
        audioSource.maxDistance = 20f; // Sound will start fading out after this distance
    }

    void Update()
    {
        if (lightBroken) return; // Skip the rest of the update if the light is broken

        // Check distance from player to the light
        float distanceToPlayer = (player.transform.position - point).magnitude;

        // If player gets too close, break the light
        if (distanceToPlayer < breakDistance)
        {
            BreakLight();
        }
        else
        {
            // If light is not broken, continue flickering
            FlickerLight();
        }
    }

    void FlickerLight()
    {
        float amplitude = Mathf.Clamp((far - (player.transform.position - point).magnitude) / near, 0, 1);
        amplitude *= amplitude * amplitude;

        time += Time.deltaTime;
        if (time >= 2 * Mathf.PI) time -= 2 * Mathf.PI;

        // Update the light intensity
        GetComponent<Light>().intensity = 20 * amplitude * Mathf.Sin(time * 20);

        // Update audio playback based on flicker intensity
        if (amplitude > 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            // Adjust volume based on amplitude (optional)
            audioSource.volume = amplitude;

            // Adjust pitch for more dynamic noise (optional)
            audioSource.pitch = Mathf.Lerp(1.0f, 2.0f, amplitude);
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        // Update the position of the AudioSource for directional sound
        audioSource.transform.position = transform.position;
    }

    void BreakLight()
    {
        // Flag to indicate the light is broken
        lightBroken = true;

        // Stop the flickering light
        GetComponent<Light>().intensity = 0;

        // Play the break noise
        if (breakNoise != null)
        {
            audioSource.clip = breakNoise;
            audioSource.Play();
        }

        // Optionally, stop all other sounds related to flickering
        if (audioSource.isPlaying && audioSource.clip == flickerNoise)
        {
            audioSource.Stop();
        }

        // Optionally, disable the AudioSource completely if you want to stop all sound
        // audioSource.enabled = false; // Uncomment if you want to stop sound completely after breaking
    }
}
