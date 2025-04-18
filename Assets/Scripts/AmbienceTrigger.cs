using UnityEngine;
using System.Collections;

public class AmbienceSwitcher : MonoBehaviour
{
    public GameObject oldAmbience;  
    public GameObject newAmbience;  
    public GameObject forestAmbience; 


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected by: " + other.name); 
        

        StartCoroutine(SwitchAmbienceWithDelay());
    }

    IEnumerator SwitchAmbienceWithDelay()
    {
        yield return new WaitForSeconds(0.7f);

        Debug.Log("Switching ambience...");

        if (oldAmbience != null)
        {
            oldAmbience.SetActive(false);
            Debug.Log("Old ambience turned off.");
        }
        else
        {
            Debug.LogWarning("Old ambience GameObject is not assigned.");
        }

        if (forestAmbience != null)
        {
            forestAmbience.SetActive(false);
            Debug.Log("Forest ambience turned off.");
        }
        else
        {
            Debug.LogWarning("Forest ambience not assigned.");
        }

        if (newAmbience != null)
        {
            newAmbience.SetActive(true);
            Debug.Log("New ambience turned on.");
        }
        else
        {
            Debug.LogWarning("New ambience not assigned.");
        }
    }
}
