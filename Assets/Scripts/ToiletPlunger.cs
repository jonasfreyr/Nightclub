using System;
using UnityEngine;
using UnityEngine.EventSystems;

internal enum PlungerStatus {
    GoingUp,
    GoingDown,
}

public class ToiletPlunger : MonoBehaviour, IDragHandler
{
    public int Counter { get; private set; }
    private PlungerStatus _status = PlungerStatus.GoingDown;

    public AudioClip plunge;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnDrag (PointerEventData eventData)
    {
        var pos = transform.position;
        if (pos.y > 440 && eventData.delta.y > 0 || pos.y < 340 && eventData.delta.y < 0) return;
        transform.position = new Vector3(pos.x, pos.y + eventData.delta.y, pos.z);
        
        var updatedPos = transform.position;
        if (_status == PlungerStatus.GoingDown && updatedPos.y < 350)
        {
            
            audioSource.PlayOneShot(plunge, 1f);
            _status = PlungerStatus.GoingUp;
            Counter++;
        }
        else if (_status == PlungerStatus.GoingUp && updatedPos.y > 420)
        {
            _status = PlungerStatus.GoingDown;
        }

    }

    public void ResetPlunger()
    {
        var pos = transform.position;
        transform.position = new Vector3(pos.x, 430, pos.z);

        Counter = 0;
        _status = PlungerStatus.GoingDown;
    }
}
