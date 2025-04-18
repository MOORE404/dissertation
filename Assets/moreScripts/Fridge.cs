using UnityEngine;

public class Fridge : MonoBehaviour
{
    public GameObject player;
    public float far = 4, near = 2;
    private Vector3 point;
    bool close = false, fin = false;

    public AudioSource shake, door;
        
    void Start() => point = transform.GetChild(0).position;

    void Update()
    {
        if (fin) return;
        float amplitude = Mathf.Clamp((far - (player.transform.position - point).magnitude) / near, 0, 1);
        amplitude *= amplitude * amplitude;

        shake.volume = amplitude;

        if ((player.transform.position - point).magnitude < near)
        {
            close = true;
            door.enabled = true;
            shake.enabled = false;
        }

        if (close)
        {
            if (GetComponent<Animator>().speed > 0) 
            {
                GetComponent<Animator>().speed -= Time.deltaTime;
                if (GetComponent<Animator>().speed > 0) GetComponent<Animator>().speed = 0;
            }
            else
            {               
                GetComponent<Animator>().speed = 1;
                GetComponent<Animator>().SetTrigger("Door");
                fin = true; 
            }
        }
        else GetComponent<Animator>().speed = amplitude;
    
    }
}
