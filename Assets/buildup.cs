using UnityEngine;
using System.Collections;

public class buildup : MonoBehaviour
{
    public GameObject newAmbience;  

    public GameObject horrorbuildup;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected by: " + other.name); // Log the object that triggers the collision
        

        newAmbience.SetActive(false);

        horrorbuildup.SetActive(true);
    }
}
