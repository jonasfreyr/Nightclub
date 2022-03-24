using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerEvent : EventPoint
{
    public GameObject[] _objects;
    
    public override void Break()
    {
        GameManager.Instance.speakersBroken = true;
        
        foreach (var speaker in _objects)
        {
            speaker.GetComponent<AudioSource>().enabled = false;
        }
    }
    
    public override void Fix()
    {
        GameManager.Instance.speakersBroken = false;
        
        foreach (var speaker in _objects)
        {
            speaker.GetComponent<AudioSource>().enabled = true;
        }
    }
}
