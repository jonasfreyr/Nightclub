using UnityEngine;
using Pathfinding;

public class CustomerBehaviour : MonoBehaviour
{
    public float thirst = 0f;
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
    public bool standing;
    
    public float lowerStandingTime = 1.3f;
    public float upperStandingTime = 6.2f;
    
    private float startedStandingTime = 0f;
    private float standDuration = 0f;
    
    private bool goingToPOI = false;
    private PointOfInterest _currentPOI;
    
    private AIDestinationSetter _targetSetter;
    public CustomerManager customerManager;
    
    private void Start()
    {
        _targetSetter = gameObject.GetComponent<AIDestinationSetter>();
        _targetSetter.targetV = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("POI")) return;

        if (other.gameObject == _currentPOI.gameObject)
        {
            if (_currentPOI.bar)
                drinking = true;
            else if (_currentPOI.bathroom)
                peeing = true;
            else if (_currentPOI.danceFloor)
                dancing = true;
            else if (_currentPOI.commonArea)
            {
                startedStandingTime = Time.time;

                standDuration = Random.Range(lowerStandingTime, upperStandingTime);
                
                standing = true;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!drinking)
        {
            thirst += thirstIncrease;

            if (thirst > 100)
                thirst = 100;
        }
            
        else
        {
            thirst -= thirstDecrease;

            if (thirst <= 0)
            {
                drinking = false;

                thirst = 0;
            }
        }


        if (!dancing)
        {
            funky += funkIncrease;
            
            if (funky > 100)
                funky = 100;
        }
            
        else
        {
            funky -= funkDecrease;
            
            if (funky <= 0)
            {
                dancing = false;

                funky = 0;
            }
        }


        if (!peeing)
        {
            mictury += micturyIncrease;
            
            if (mictury > 100)
                mictury = 100;
        }

        else
        {
            mictury -= micturyDecrease;
            
            if (mictury <= 0)
            {
                peeing = false;

                mictury = 0;
            }
        }


        
        if (!puking)
        {
            pukeness += pukeIncrease;
            
            if (pukeness > 100)
                pukeness = 100;
        }

        else
        {
            pukeness -= pukeDecrease;
            
            if (pukeness <= 0)
            {
                puking = false;

                pukeness = 0;
            }
        }

        if (standing)
        {
            if (startedStandingTime + standDuration < Time.time)
            {
                standing = false;
            }
        }
        
        if (_targetSetter.done)
            goingToPOI = false;
        
        if (goingToPOI || drinking || peeing || dancing || puking || standing) return;
        
        PointOfInterest target;
        
        if (pukeness >= pukeThreshold)
        {
            target = customerManager.GetRandomPOI(customerManager.bathrooms);
        }
        else if (mictury >= micturyThreshold)
        {
            target = customerManager.GetRandomPOI(customerManager.bathrooms);
        }
        else if (thirst >= thirstThreshold)
        {
            target = customerManager.GetRandomPOI(customerManager.bars);
        }
        else if (funky >= funkThreshold)
        {
            target = customerManager.GetRandomPOI(customerManager.danceFloors);
        }
        else
        {
            target = customerManager.GetRandomPOI(customerManager.commonAreas);
        }

        
        _currentPOI = target;
        _targetSetter.targetV = target.GetRandomPointWithinCollider();
        goingToPOI = true;
 
    }
}
