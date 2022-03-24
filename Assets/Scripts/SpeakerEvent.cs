using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerEvent : EventPoint
{
    public GameObject[] _objects;
    private bool _isBroken;


    private BoxCollider2D _collider;
    
    public void Start()
    {
        _collider = gameObject.GetComponent<BoxCollider2D>();
    }
    
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
        GameManager.Instance.speakersBroken = false;
        
        foreach (var speaker in _objects)
        {
            speaker.GetComponent<AudioSource>().enabled = true;
        }

        _isBroken = false;
        repairStatusCanvas.SetActive(false);
    }

    public override bool IsBroken()
    {
        return _isBroken;
    }

    public override Vector3 GetEventPosition()
    {
        var size = _collider.size;
        var position = (Vector2) transform.position + _collider.offset;
        var x = Random.Range(position.x - (size.x / 2), position.x + (size.x / 2));
        var y = Random.Range(position.y - (size.y / 2), position.y + (size.y / 2));

        return new Vector3(x, y);
    }
}
