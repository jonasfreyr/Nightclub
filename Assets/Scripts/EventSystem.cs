using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EventSystem : MonoBehaviour
{
    public EventPoint[] _eventPoints;
    private float _timer = 0f;
    public float interval = 5f;

    private void DisableObject()
    {
        var subObjects = Array.FindAll(_eventPoints, o => !o.IsBroken());
    
        if (subObjects.Length > 0) 
            subObjects[Random.Range(0, subObjects.Length)].Break();
    }
    
    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        
        
        if (_timer >= interval && GameManager.Instance.IsNightTime)
        {
            _timer = 0f;

            DisableObject();
        }
    }
}
