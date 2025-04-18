using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CarScript : MonoBehaviour
{
    public AudioSource CrashAudio;
    public AudioSource EngineRev;
    public AudioSource GeneralEngineSound;

    //public CanvasGroup CG;
    
    public Renderer BlackoutRenderer;
    private Color BlackoutStartColor;

    private float fadeDuration = 1f;

    private float Timer;
    private bool EngineRevved = false;
    private bool crashed = false;
    private bool BlackedOut = false;

    public GameObject PC;
    public Vector3 targetLocalPosition;

    public void Start()
    {
        
        if (BlackoutRenderer != null)
        {
            
            BlackoutStartColor = BlackoutRenderer.material.color;

            
            Color transparentColor = BlackoutStartColor;
            transparentColor.a = 0f;
            BlackoutRenderer.material.color = transparentColor;
        }
    }

    public void Update()
    {
        Timer += Time.deltaTime;
        if(Timer > 25 && !EngineRevved)
        {
            EngineRevved = true;
            EngineRev.Play();
        }
        if (Timer > 32f && !crashed)
        {
            crashed = true;
            CrashAudio.Play();  
        }
        if(Timer > 33 && !BlackedOut)
        {
            StartCoroutine(FadeImageAlpha(0, 255));
            EngineRev.Stop();
            GeneralEngineSound.Stop();
        }
        if(Timer > 37)
        {
            SceneManager.LoadScene("GameLevel");
        }
    }

    public void LateUpdate()
    {
        Quaternion currentRotation = transform.rotation;

        
        transform.rotation = Quaternion.Euler(0, currentRotation.eulerAngles.y, 0);

        PC.transform.localPosition = targetLocalPosition;
    }










    public IEnumerator FadeImageAlpha(float startAlpha, float endAlpha)
    {

     

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            
            float alphaProgress = elapsedTime / fadeDuration;
            //CG.alpha = startAlpha + (endAlpha - startAlpha) * alphaProgress; 

            Color newColor = BlackoutStartColor;
            newColor.a = alphaProgress;
            BlackoutRenderer.material.color = newColor;

            Debug.Log("progress is " + alphaProgress + " and the color alpha will be " + BlackoutRenderer.material.color.a);
            
            

            yield return null; 
        }

    
    }
}
