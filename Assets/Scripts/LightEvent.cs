using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEvent : EventPoint
{
    public override void Break()
    {
        foreach (GameObject child in transform)
        {
            child.SetActive(false);
        }
    }

    public override void Fix()
    {
        foreach (GameObject child in transform)
        {
            child.SetActive(true);
        }
    }
}
