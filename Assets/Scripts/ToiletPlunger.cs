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

    public void OnDrag (PointerEventData eventData)
    {
        var pos = transform.position;
        if (pos.y > 590 && eventData.delta.y > 0 || pos.y < 520 && eventData.delta.y < 0) return;
        transform.position = new Vector3(pos.x, pos.y + eventData.delta.y, pos.z);
        
        var updatedPos = transform.position;
        if (_status == PlungerStatus.GoingDown && updatedPos.y < 530)
        {
            _status = PlungerStatus.GoingUp;
            Counter++;
        }
        else if (_status == PlungerStatus.GoingUp && updatedPos.y > 570)
        {
            _status = PlungerStatus.GoingDown;
        }

    }

    public void ResetPlunger()
    {
        var pos = transform.position;
        transform.position = new Vector3(pos.x, 580, pos.z);

        Counter = 0;
        _status = PlungerStatus.GoingDown;
    }
}
