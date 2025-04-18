using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CheckForMonsterLooking : MonoBehaviour
{
    public GameObject LightForFinalMonster;

    private float fadeDuration = 3f;
    public Renderer BlackoutRenderer;
    private Color BlackoutStartColor;
    public GameObject FadeOut;

    public LayerMask Monster;
    int monsterLayerMask;
    public GameObject GoForward;
    public float Delay;
    private bool HasLookedAtMonster = false;
    private bool HasInitiatedFinalJumpscare = false;

    private bool TimeToDoFadeOut;

    public AudioSource PiggieTurnOff;
    public AudioSource Scream;

    public void Start()
    {
        monsterLayerMask = LayerMask.GetMask("MonsterCheck");

        if (BlackoutRenderer != null)
        {

            BlackoutStartColor = BlackoutRenderer.material.color;


            Color transparentColor = BlackoutStartColor;
            transparentColor.a = 0f;
            BlackoutRenderer.material.color = transparentColor;
        }
    }

    void Update()
    {
        
        if (Physics.Raycast(transform.position, transform.forward, 1000f , monsterLayerMask, QueryTriggerInteraction.Collide ))
        {
            Debug.Log("has looked at monster");
            //Debug.DrawRay(transform.position, transform.forward * 100, Color.green, 5f);
            HasLookedAtMonster = true;
        }
        if (HasLookedAtMonster)
        {
            Delay -= Time.deltaTime;
            if(Delay < 0 && HasInitiatedFinalJumpscare == false)
            {
                HasInitiatedFinalJumpscare = true;

                FadeOut.SetActive(true); 
                Color transparentColor = BlackoutStartColor;
                transparentColor.a = 0f;
                BlackoutRenderer.material.color = transparentColor;
                
                Debug.Log("calling fade out");
                StartCoroutine(FadeImageAlpha(0, 255));
                GoForward.gameObject.SetActive(true);
                PiggieTurnOff.gameObject.SetActive(false);
                Scream.Play();
                LightForFinalMonster.SetActive(true);
            }

            
        }

    }



    /*
    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * 1000, Color.blue);
    }
    */


    public IEnumerator FadeImageAlpha(float startAlpha, float endAlpha)
    {
        Debug.Log("fading out");
        

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

        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("Credits");


    }
}
