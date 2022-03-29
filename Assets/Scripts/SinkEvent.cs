using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkEvent : EventPoint
{
    public float fixTime;
    private bool _isBroken;
    private bool _isFixing;
    
    public override void Break()
    {
        GameManager.Instance.speakersBroken = true;

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
        _setRepairButtonState(true);
        yield return new WaitForSeconds(fixTime);

        _isBroken = false;
        _isFixing = false;
        repairStatusCanvas.SetActive(false);
        _setRepairButtonState(false);
    }

    public override Vector3 GetEventPosition()
    {
        return transform.position;
    }

    public override bool IsBroken()
    {
        return _isBroken;
    }
}
