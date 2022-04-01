using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EventSystem : MonoBehaviour
{
    public EventPoint[] _eventPoints;
    public float _timer = 0f;
    public float interval = 5f;
    public int maxBreak = 3;
    private void DisableObject()
    {
        var subObjects = Array.FindAll(_eventPoints, o => !o.IsBroken());
    
        if (subObjects.Length > 0) 
            subObjects[Random.Range(0, subObjects.Length)].Break();
    }

    public void FixAll()
    {
        foreach (var events in _eventPoints)
        {
            events.ForceFix();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        var subobjects = Array.FindAll(_eventPoints, o => !o.IsBroken());
        
        if (subobjects.Length == 0 || (_eventPoints.Length - subobjects.Length) == maxBreak)
        {
            _timer = 0f;
        }
        
        if (_timer >= interval && GameManager.Instance.IsNightTime)
        {
            _timer = 0f;

            DisableObject();
        }
    }
}
