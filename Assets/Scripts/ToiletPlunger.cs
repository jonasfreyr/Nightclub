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
    private float? bottomPosition;
    private float? topPosition;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnDrag (PointerEventData eventData)
    {
        var pos = transform.position;
        if (pos.y > topPosition && eventData.delta.y > 0 || pos.y < bottomPosition && eventData.delta.y < 0) return;
        transform.position = new Vector3(pos.x, pos.y + eventData.delta.y, pos.z);
        
        var updatedPos = transform.position;
        if (_status == PlungerStatus.GoingDown && updatedPos.y < bottomPosition + 10)
        {
            
            audioSource.PlayOneShot(plunge, 1f);
            _status = PlungerStatus.GoingUp;
            Counter++;
        }
        else if (_status == PlungerStatus.GoingUp && updatedPos.y > topPosition - 20)
        {
            _status = PlungerStatus.GoingDown;
        }

    }

    public void ResetPlunger()
    {
        if (bottomPosition == null)
        {
            bottomPosition = transform.position.y - 50;
            topPosition = transform.position.y + 50;
        }
        
        var pos = transform.position;
        transform.position = new Vector3(pos.x, topPosition.Value + 10, pos.z);

        Counter = 0;
        _status = PlungerStatus.GoingDown;
    }
}
