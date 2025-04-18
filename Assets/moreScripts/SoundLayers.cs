using System;
using UnityEngine;
using UnityEngine.XR;

public class SoundLayers : MonoBehaviour
{
    public LayerMask wood, stairs, terrain;
    public AudioSource woodSfx, stairsSfx, terrainSfx;
    private LayerMask[] layers;
    


    public AudioClip[] WoodFloorClips;
    public AudioClip[] StairsClips;
    public AudioClip[] TerrainClips;
    private Vector3 lastPosition;
    private float accumulatedDistance;
    private float stepDistance = 1f;

    private void Start() 
    {
        layers = new LayerMask[] { wood, stairs, terrain };
        
        
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        float distanceMoved = Vector3.Distance(
            new Vector3(currentPosition.x, 0, currentPosition.z),
            new Vector3(lastPosition.x, 0, lastPosition.z)
        );


        accumulatedDistance += distanceMoved;


        if (accumulatedDistance >= stepDistance)
        {

            accumulatedDistance = 0f;

            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo2, 10f, stairs))
            {
                stairsSfx.PlayOneShot(StairsClips[UnityEngine.Random.Range(0, StairsClips.Length)]);
            }
            else if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo1, 10f, wood))
            {
                woodSfx.PlayOneShot(WoodFloorClips[UnityEngine.Random.Range(0, WoodFloorClips.Length )]);
            }
            else if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo3, 10f, terrain))
            {
                terrainSfx.PlayOneShot(TerrainClips[UnityEngine.Random.Range(0, TerrainClips.Length )]);
            }
        }

        lastPosition = currentPosition;
    }


}
