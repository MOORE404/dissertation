using UnityEngine;

public class BranchSoundTrigger : MonoBehaviour
{
    public AudioSource branch;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            branch.Play();            
            GetComponent<Collider>().enabled = false;
        }
    }
}
