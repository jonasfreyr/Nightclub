using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CustomerBehaviour : MonoBehaviour
{
    public float thrist = 0f;
    public float funky = 0f;
    public float mictury = 0f;
    public float pukeness = 0f;
    
    public float thirstThreshold = 80f;
    public float funkThreshold = 80f;
    public float micturyThreshold = 80f;
    public float pukeThreshold = 80f;

    public float thirstIncrease = 0.2f;
    public float funkIncrease = 0.2f;
    public float micturyIncrease = 0.2f;
    public float pukeIncrease = 0.2f;
    
    public float thirstDecrease = 0.2f;
    public float funkDecrease = 0.2f;
    public float micturyDecrease = 0.2f;
    public float pukeDecrease = 0.2f;

    public bool drinking;
    public bool dancing;
    public bool peeing;
    public bool puking;

    private bool goingToPOI = false;
    
    private AIDestinationSetter _targetSetter;
    public CustomerManager customerManager;
    
    private void Start()
    {
        _targetSetter = gameObject.GetComponent<AIDestinationSetter>();
        _targetSetter.targetV = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        thrist += thirstIncrease;

        if (goingToPOI) return;
        
        if (pukeness >= pukeThreshold)
        {
            
        }
        else if (mictury >= micturyThreshold)
        {
            
        }
        else if (thrist >= thirstThreshold)
        {
            var target = customerManager.GetRandomPOIPosition(customerManager.bars);

            _targetSetter.targetV = target;
            
            goingToPOI = true;
        }
        else if (funky >= funkThreshold)
        {
            
        }
        else
        {
            _targetSetter.targetV = null;
        }
        
        
    }
}
