using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerEvent : EventPoint
{
    public GameObject[] _objects;
    public float fixTime;
    private bool _isBroken;
    private bool _isFixing;
    
    public override void Break()
    {
        GameManager.Instance.speakersBroken = true;
        
        foreach (var speaker in _objects)
        {
            speaker.GetComponent<AudioSource>().enabled = false;
        }

        _isBroken = true;
        repairStatusCanvas.SetActive(true);
    }
    
    public override void Fix()
    {
        if (_isFixing) return;
        _isFixing = true;
        StartCoroutine(_fix());
    }

    private IEnumerator _fix()
    {
        yield return new WaitForSeconds(fixTime);
        GameManager.Instance.speakersBroken = false;
        
        foreach (var speaker in _objects)
        {
            speaker.GetComponent<AudioSource>().enabled = true;
        }

        _isBroken = false;
        _isFixing = false;
        repairStatusCanvas.SetActive(false);
    }

    public override bool IsBroken()
    {
        return _isBroken;
    }
}
