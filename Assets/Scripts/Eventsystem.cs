using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class Eventsystem : MonoBehaviour
{
    private bool isHallucinating;
    private float HallucinateTimer = 4f;
    private float HallucinateTimerMax = 4f;
    public AudioSource HeartbeatAudio;

    
    public GameObject FadeOutObject;



    public Action OnJumpScare;
    public GameObject HallucinagenicPostProcess;

    [Header("References")]
    public GameObject player;
    public Transform CameraPosition;
    public EventSignaller[] EventObjectReferences;
    public GameObject RandomAudioGameObject;

    public FlashlightFlicker flashlightS;


    [Header("Floor 1")]
    public AudioSource MonsterGrowl;
    public Animator CabinetAnimator;
    public AudioSource CabinetAudio;
    public Animator FrontDoorAnimator;
    public AudioSource FrontDoorAudio;
    public Animator StairChairAnimator;
    public AudioSource StairChairAudioSource;
    public Animator OuijaBoardAnimator;
    public AudioSource OuijaBoardAudio;
    public AudioSource BranchSnap1;
    public GameObject StairChairCollider;
    public GameObject WindowEyes;
    public Animator FridgeAnimator;
    public AudioSource FridgeAudio;
    public GameObject OuijaBoard;
    public AudioSource FrontDoorCloseCreak;
    public AudioSource FrontDoorSlam;
    public AudioSource FrontDoorScary;
    

    [Header("Floor 2")]

    public AudioSource SecondFloorBlockaudioSource;
    public AudioSource SeconddooraudioSource;
    public Animator DecalAnimator;
    public Animator BathroomDoorAnimator;
    public Animator BedroomDoorAnimator;
    public Animator VoodooAnimator;
    public GameObject voodoo1;
    public GameObject voodoo2;
    public AudioSource DollAudio;
    public AudioSource MonsterMunchingAudio;

    public List<GameObject> blood;

    public List<Rigidbody> ToiletPaperRB;
    public Animator SecondFloorBlockAnimator;

    private List<bool> eventChecklist = new List<bool>();
    private const int TotalFloor1Events = 6;

    [Header("Floor 3")]

    public GameObject MonsterTarget;
    public GameObject Monster;
    public GameObject CheckForLookingAtMonster;

    private void Awake()
    {
        InitializeEventChecklist();
        InitializeEventHandlers();
        InitializeOuijaBoard();

        HallucinagenicPostProcess.SetActive(false);

        FadeOutObject.SetActive(false);
    }

    private void InitializeEventChecklist()
    {
        for (int i = 0; i < EventObjectReferences.Length; i++)
        {
            eventChecklist.Add(false);
        }
    }

    private void InitializeEventHandlers()
    {
        foreach (EventSignaller script in EventObjectReferences)
        {
            script.HorrorEvent += HandleHorrorEvent;
        }
    }

    private void InitializeOuijaBoard()
    {
        OuijaBoard.transform.position = new Vector3(-4.959f, 2.8f, -5.2f);
        OuijaBoard.transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    private void HandleHorrorEvent(string eventName)
    {
        switch (eventName)
        {

            case "BranchSnap1":
                TriggerBranchSnap1();
                break;

            case "MonsterGrowl":
                TriggerMonsterGrowl();
                break;

            case "FrontDoor":
                TriggerFrontDoor();
                break;

            case "CloseFrontDoor":
                TriggerCloseFrontDoor();
                break;

            case "KitchenCompleted":
                TriggerKitchenCompleted();
                break;

            case "OuijaBoard":
                TriggerOuijaBoard();
                break;

            case "StairChairTrigger":
                TriggerStairChair();
                break;

            case "SecondFloorBlock":
                TriggerSecondFloorBlock();
                break;


            case "DecalMover":
                TriggerDecalMover();
                break;

              case "BedroomDoor":
                TriggerBedroomDoor();
                break;

            case "Voodoo":
                TriggerVoodoo();
                break;
            
            case "ToiletPaperTrigger":
                TriggerToiletPaper();
                break;

            case "BathroomJumpScare":
                TriggerJumpScare();
                break;

            case "FinalScare":
                TriggerFinalScare();
                break;

            default:
                Debug.LogWarning($"Unhandled event: {eventName}");
                break;
        }
    }

    private bool AreAllFloor1EventsComplete()
    {
        for (int i = 0; i < TotalFloor1Events; i++)
        {
            if (!eventChecklist[i])
            {
                return false;
            }
        }
        return true;
    }


    private void TriggerBranchSnap1()
    {
        if (!eventChecklist[0])
        {
            BranchSnap1.Play();
            eventChecklist[0] = true;
            Debug.Log("Triggered Branch snap");
        }
    }

    private void TriggerMonsterGrowl()
    {
        MonsterGrowl.Play();
        Debug.Log("Triggered MonsterGrowl");
    }

    private void TriggerFrontDoor()
    {
        if (!eventChecklist[1])
        {
            FrontDoorAnimator.Play("DoorOpen");
            FrontDoorAudio.Play();
            WindowEyes.SetActive(false);
            FridgeAnimator.enabled = true;
            eventChecklist[1] = true;
            flashlightS.minIntensity = 0.01f;
            flashlightS.inHouse = true;
            Debug.Log("Triggered Dooropen");
        }
    }

    private void TriggerCloseFrontDoor()
    {
        if (!eventChecklist[2])
        {
            StartCoroutine(FrontDoorCloseRoutine());
            eventChecklist[2] = true;
            Debug.Log("Triggered close door");
        }
    }

    private void TriggerKitchenCompleted()
    {
        if (!eventChecklist[3])
        {
            FridgeAnimator.Play("FridgeDoor");
            FridgeAudio.Stop();
            CabinetAnimator.enabled = true;
            CabinetAudio.Play();
            OuijaBoardAnimator.enabled = true;
            eventChecklist[3] = true;

            isHallucinating = true;

            Debug.Log("Triggered Kithen Events");
        }
    }

    private void TriggerOuijaBoard()
    {
        if (!eventChecklist[4] && eventChecklist[3])
        {
            OuijaBoardAnimator.enabled = false;
            OuijaBoard.GetComponent<Rigidbody>().useGravity = true;
            StartCoroutine(OuijaBoardFallRoutine());
            eventChecklist[4] = true;
            Debug.Log("Triggered Quija");
        }
    }

    private void TriggerStairChair()
    {
        if (!eventChecklist[5] && eventChecklist[4])
        {
            StairChairAnimator.enabled = true;
            StairChairAudioSource.Play();
            StairChairCollider.SetActive(false);
            eventChecklist[5] = true;
            Debug.Log("First Floor Completed");
        }
    }

    private void TriggerDecalMover()
    {
        if (!eventChecklist[6])
        {
            DecalAnimator.enabled = true;
            eventChecklist[6] = true;
            Debug.Log("Triggered decal mover");
        }
    }

    private void TriggerSecondFloorBlock()
    {
        if (!eventChecklist[7])
        {
            SecondFloorBlockAnimator.enabled = true;
            SecondFloorBlockaudioSource.Play();
            eventChecklist[7] = true;
            Debug.Log("Triggered Second Floor Blockage");
        }
    }

    private void TriggerBedroomDoor()
    {
        if (!eventChecklist[8])
        {
            BedroomDoorAnimator.enabled = true;
            SeconddooraudioSource.Play();
            eventChecklist[8] = true;
            Debug.Log("Triggered Bedroom Door");

        }
    }

        private void TriggerVoodoo()
    {
        if (!eventChecklist[9])
        {
            DollAudio.Play();
            voodoo1.SetActive(false);
            voodoo2.SetActive(true);
            VoodooAnimator.enabled = true;
            eventChecklist[9] = true;
            Debug.Log("Triggered Voodoo");

            isHallucinating = true;

            MonsterMunchingAudio.Play();
            foreach (GameObject bloodstain in blood)
            {
                bloodstain.SetActive(true);
            } 
            BathroomDoorAnimator.enabled = true;
        }
    }

        private void TriggerToiletPaper()
        {
            if (!eventChecklist[10])
            {
                foreach (Rigidbody RB in ToiletPaperRB)
                {
                    RB.AddForce(new Vector3(0, 0, 3), ForceMode.Impulse);
                }

                eventChecklist[10] = true;
            }
            else
            {
                Debug.Log("Toilet paper trigger has already been activated.");
            }
        }


        private void TriggerJumpScare()
        {
            if (!eventChecklist[11])
            {
                OnJumpScare.Invoke();
            SecondFloorBlockAnimator.Play("SecondFloorCompleted");
            }
        }

        private void TriggerFinalScare()
        {
            Monster.SetActive(true);
            CheckForLookingAtMonster.SetActive(true);
        }


    public void Update()
    {
        if(isHallucinating)
        {
            HallucinagenicPostProcess.SetActive(true);
            HallucinateTimer -= Time.deltaTime;
            
            if(HallucinateTimer < 0)
            {
                isHallucinating = false;
                HallucinateTimer = HallucinateTimerMax;
                HallucinagenicPostProcess.SetActive(false);
            }

            if (!HeartbeatAudio.isPlaying)
            {
                HeartbeatAudio.Play();
            }

        }
    }
   
    

    private IEnumerator FrontDoorCloseRoutine()
    {
        FrontDoorAnimator.Play("FrontDoorClose");
        FrontDoorCloseCreak.Play();
        yield return new WaitForSeconds(0.4f);
        FrontDoorSlam.Play();
        FrontDoorScary.Play();
        yield return new WaitForSeconds(4);
        RandomAudioGameObject.SetActive(true);
    }

    private IEnumerator OuijaBoardFallRoutine()
    {
        yield return new WaitForSeconds(0.8f);
        OuijaBoardAudio.Play();
    }

    private IEnumerator DestroyShrek(GameObject shrek)
    {
        yield return new WaitForSeconds(5f);
        Destroy(shrek);
    }
}
