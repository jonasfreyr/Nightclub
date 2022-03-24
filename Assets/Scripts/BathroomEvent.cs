using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomEvent : EventPoint
{

    public override void Break()
    {
        gameObject.GetComponent<PointOfInterest>().enabled = false;
    }
    
    public override void Fix()
    {
        gameObject.GetComponent<PointOfInterest>().enabled = true;
    }
}
