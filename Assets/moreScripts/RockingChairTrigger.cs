using UnityEngine;

public class RockingChairTrigger : MonoBehaviour
{
    public GameObject chair;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            chair.GetComponent<Animator>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
