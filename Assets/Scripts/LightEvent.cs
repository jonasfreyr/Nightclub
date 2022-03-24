using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEvent : EventPoint
{
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
        foreach (GameObject child in transform)
        {
            child.SetActive(true);
        }

        _isBroken = false;
    }

    public override bool IsBroken()
    {
        return _isBroken;
    }
}
