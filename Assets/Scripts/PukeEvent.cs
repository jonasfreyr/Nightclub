using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PukeEvent : EventPoint
{
    public float cleaningTime;
    private bool _isBroken;
    private bool _isFixing;

    void Start()
    {
        Break();
    }
    
    public override void Break()
    {
        _isBroken = true;
        GameManager.Instance.pukeOnFloor = true;
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
        yield return new WaitForSeconds(cleaningTime);

        GameManager.Instance.pukeOnFloor = false;
        _isBroken = false;
        _isFixing = false;
        _setRepairButtonState(false);
        repairStatusCanvas.SetActive(false);
        Destroy(gameObject, 1);
    }
    
    public override Vector3 GetEventPosition()
    {
        return transform.position;
    }

    public override bool IsBroken()
    {
        return _isBroken;
    }

    public override bool IsFixing()
    {
        return _isFixing;
    }
}
