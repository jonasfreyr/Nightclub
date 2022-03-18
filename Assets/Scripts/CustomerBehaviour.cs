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

    public bool drinking;
    public bool dancing;
    public bool peeing;
    public bool standing;
    
    public float lowerStandingTime = 1.3f;
    public float upperStandingTime = 6.2f;
    
    private float startedStandingTime = 0f;
    private float standDuration = 0f;
    
    public bool goingToPOI = false;
    
    private bool wasStanding = false;
    private bool wasDancing = false;
    private bool wasDrinking = false;
    private bool wasPeeing = false;
    
    public bool finished_task = true;
    public PointOfInterest _currentPOI;
    
    private AIDestinationSetter _targetSetter;
    public CustomerManager customerManager;
    public GameObject puke;
    
    public Animator animator;

    private static readonly int Dancing = Animator.StringToHash("Dancing");
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Standing = Animator.StringToHash("Standing");

    private void Start()
    {
        _targetSetter = gameObject.GetComponent<AIDestinationSetter>();
        _targetSetter.targetV = null;
    }

    private bool DoingSomething()
    {
        return drinking || peeing || dancing || standing;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (goingToPOI && _targetSetter.done)
        {
            goingToPOI = false;
            
            if (_currentPOI.bar)
            {
                drinking = true;
            }
            else if (_currentPOI.bathroom)
            {
                peeing = true;
            }
            else if (_currentPOI.danceFloor)
            {
                dancing = true;
            }
            else if (_currentPOI.commonArea)
            {
                startedStandingTime = Time.time;

                standDuration = Random.Range(lowerStandingTime, upperStandingTime);
                
                standing = true;
            }
        }
        
        if (!GameManager.Instance.IsNightTime)
        {
            _targetSetter.SetTarget(customerManager.GetRandomSpawnpoint(customerManager.spawnPoints).position);
            goingToPOI = true;
            
            if (_targetSetter.done)
            {
                Destroy(gameObject);
            }
            
            return;
        }
            

        if (peeing)
        {
            if (peeing != wasPeeing)
                animator.SetTrigger(Standing);
            
            mictury -= micturyDecrease;
            
            if (mictury <= 0)
            {
                peeing = false;
                finished_task = true;
                mictury = 0;
            }
        }
        
        if (!drinking)
        {
            thirst += thirstIncrease;

            if (thirst > 100)
                thirst = 100;
        }
            
        else if (drinking)
        {   
            if (drinking != wasDrinking)
                animator.SetTrigger(Standing);
            
            thirst -= thirstDecrease;
            mictury += micturyIncrease;
            pukeness += pukeIncrease;
            
            if (pukeness > 100)
                pukeness = 100;
            
            if (mictury > 100)
                mictury = 100;
            
            if (thirst <= 0)
            {
                drinking = false;
                finished_task = true;
                thirst = 0;
            }
        }
        
        if (!dancing)
        {

            funky += funkIncrease;

            if (funky > 100)
                funky = 100;
        }
            
        else if (dancing)
        {
            if (dancing != wasDancing) 
                animator.SetTrigger(Dancing);
            
            funky -= funkDecrease;
            thirst += thirstIncrease;

            if (thirst > 100)
                thirst = 100;
            
            if (funky <= 0)
            {
                dancing = false;
                finished_task = true;
                funky = 0;
            }
        }

        if (standing)
        {
            if (standing != wasStanding)
                animator.SetTrigger(Standing);
            
            if (startedStandingTime + standDuration < Time.time)
            {
                standing = false;
                finished_task = true;
            }
        }

        wasStanding = standing;
        wasDancing = dancing;
        wasPeeing = peeing;
        wasDrinking = drinking;
        
        if (DoingSomething() || goingToPOI || !finished_task) return;
        
        PointOfInterest target = null;
        
        if (pukeness >= pukeThreshold)
        {
            Instantiate(puke, transform.position, Quaternion.identity);
            pukeness = 0f;

            startedStandingTime = Time.time;

            standDuration = Random.Range(lowerStandingTime, upperStandingTime);
                
            standing = true;
            
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

        if (target != null)
        {
            _currentPOI = target;
            _targetSetter.targetV = target.GetRandomPointWithinCollider();
            goingToPOI = true;
            finished_task = false;
            animator.SetTrigger(Walking);
        }
    }
}
