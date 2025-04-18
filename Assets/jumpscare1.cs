using System.Collections;
using UnityEngine;

public class jumpscare1 : MonoBehaviour
{
    public Eventsystem ES;
    public GameObject Camera;
    public GameObject Monster;
    public GameObject Wall;
    public GameObject Light;
    public AudioSource AS;

    public GameObject MonsterGrunting;

    public GameObject PlayerCam;

    public GameObject BathroomMonster;

    public void Awake()
    {
        ES.OnJumpScare += DoJumpScare;
    }

    public void DoJumpScare()
    {
        MonsterGrunting.SetActive(false);
        BathroomMonster.SetActive(false);   
        Camera.SetActive(true);
        Monster.SetActive(true);
        Wall.SetActive(true);
        Light.SetActive(true);

        PlayerCam.SetActive(false);

        AS.Play();

        StartCoroutine(EndJumpScare());
    }

    public IEnumerator EndJumpScare()
    {
        yield return new WaitForSeconds(1.75f);

        Camera.SetActive(false);
        Monster.SetActive(false);   
        Wall.SetActive(false);
        Light.SetActive(false);

        PlayerCam.SetActive(true);
        
    }
}
