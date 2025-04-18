using UnityEngine;
using System;

public class EventSignaller : MonoBehaviour
{
    public string EventName;

    public Action <string> HorrorEvent;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player")
        {
            HorrorEvent?.Invoke(EventName);
        }
        
    }


}
