using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEvent : EventPoint
{
    public float fixTime;
    private bool _isFixing;
    private bool _isBroken;
    
    public override void Break()
    {
        foreach (GameObject child in transform)
        {
            child.SetActive(false);
        }

        _isBroken = true;
    }

    public override void Fix()
    {
        if (!_isFixing) return;
        _isFixing = true;

        StartCoroutine(_fix());

        _isBroken = false;
    }
    
    private IEnumerator _fix()
    {
        yield return new WaitForSeconds(fixTime);
        GameManager.Instance.speakersBroken = false;
        
        foreach (GameObject child in transform)
        {
            child.SetActive(true);
        }

        _isBroken = false;
        _isFixing = false;
    }

    public override bool IsBroken()
    {
        return _isBroken;
    }
}
