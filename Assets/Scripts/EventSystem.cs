using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public EventPoint[] _eventPoints;
    private float _timer = 0f;
    public float interval = 5f;

    private void DisableObject()
    {
        _eventPoints[Random.Range(0, _eventPoints.Length)].Brake();
    }
    
    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        
        
        if (_timer >= interval)
        {
            _timer = 0f;

            DisableObject();
        }
    }
}
