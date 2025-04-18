using UnityEngine;
using System.Collections;
using UnityEditor;

public class FlashlightFlicker : MonoBehaviour
{
    public Light flashlight;

    public AudioClip onSound;
    public AudioClip offSound;
    public AudioSource lightFlicker;

    



    private float minFlickerDelay = 0.35f; 
    private float maxFlickerDelay = 2f;  
    public float minIntensity = 50;   
    private float maxIntensity = 50;     


    private bool isFlickering = false;   

    private float charge = 110f;
    private float chargeIntensity = 4f;
    private float chargeFlickerTime = 0.5f;
    private Vector3 prevPos;

    public bool inHouse;

    private float delay = 1f;

    private int OnOff;

    void Start()
    {
        if (flashlight == null)
        {
            flashlight = GetComponent<Light>();
        }

        if (flashlight == null)
        {
            Debug.LogError("No Light component found on this GameObject.");
        }
    }

    void Update()
    {
        if ( delay <= 0 && flashlight != null && inHouse)
        {
            
            delay = Random.Range(minFlickerDelay, maxFlickerDelay); 
        

        chargeIntensity = charge / 25;
            
            Debug.Log("chargeIntensity is: " + chargeIntensity + " charge is: " + charge);
            if (charge > 55)
            {
                flashlight.intensity = chargeIntensity + 8; // can note this out if I want to keep the flashlight strength and only use the flashing effect.
            }
            else if (charge < 55 && charge > 20)
            {

                //flashlight.intensity = Random.Range(minIntensity, chargeIntensity +1); old system just changed flashlight strength rather than completely on / off
                OnOff = Random.Range(0, 2);
                Debug.Log(OnOff);
                if(OnOff == 0)
                {
                    lightFlicker.PlayOneShot(onSound);
                    flashlight.intensity = 0;
                }
                else
                {
                    lightFlicker.PlayOneShot(offSound);
                    flashlight.intensity = chargeIntensity + 8;
                }
            }
            else if (charge < 20f)
            {
                flashlight.intensity = 0f;
            }
        
        }

        delay -= Time.deltaTime;
        Debug.Log("delay");




        

        if (charge > 110)
        {
            charge = 110;
        }

        if (inHouse)
        {
            Debug.Log("isInHouse running flashlight decharge");
            charge -= (Time.deltaTime * 3f);
            if (charge <= 10)
            {
                charge = 10;
            }
        }


        float yDifference = Mathf.Abs(transform.position.y - prevPos.y);
        Debug.Log(yDifference);
        charge += (yDifference * 5f);
        

        prevPos = transform.position;
        

    }

    /*
    private IEnumerator Flicker()
    {
        chargeIntensity = charge / 25;
        //chargeFlickerTime = charge / 333.333f;
        Debug.Log("chargeIntensity is: " + chargeIntensity + " charge is: " + charge);
        if(charge > 45)
        {
            flashlight.intensity = chargeIntensity;
        }
        else if (charge < 45 && charge > 20)
        {
            flashlight.intensity = Random.Range(minIntensity, chargeIntensity);
        }
        else if(charge < 20f)
        {
            flashlight.intensity = 0f;
        }
        
        

        
        float delay = Random.Range(minFlickerDelay, chargeFlickerTime);
        yield return new WaitForSeconds(delay);

        isFlickering = false;
    }
    */ //old system (coroutines aren't great due to monothreaded execution)
}
