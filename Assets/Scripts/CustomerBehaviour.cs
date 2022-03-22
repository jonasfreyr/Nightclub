using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
using System.Collections.Generic;

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

    private int maxAttr = 100;
    public float cozy;
    public float romantic;
    public float chaotic;
    public float satisfaction;

    private void Start()
    {
        _targetSetter = gameObject.GetComponent<AIDestinationSetter>();
        _targetSetter.targetV = null;
        generateCustomerStats();
    }

    private bool DoingSomething()
    {
        return drinking || peeing || dancing || standing;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        if (!GameManager.Instance.IsNightTime)
        {
            _targetSetter.SetTarget(customerManager.GetRandomSpawnpoint(customerManager.spawnPoints).position);
            goingToPOI = true;
            
            if (_targetSetter.done)
            {
                generateReview();
                Destroy(gameObject);
            }
            
            return;
        }
        
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
            var pukeObject = Instantiate(puke, transform.position, Quaternion.identity);
            pukeness = 0f;

            startedStandingTime = Time.time;

            standDuration = Random.Range(lowerStandingTime, upperStandingTime);
                
            standing = true;
            
            GameManager.Instance.employeeManager.tasks.Add(new EmployeeTaskInfo
            {
                name = "Clean puke",
                task = EmployeeTask.CleanPuke,
                argument = pukeObject
            });
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

    private void generateCustomerStats() {
        // TODO: Add Synergies between customer stats ?
        cozy = Random.Range(0, maxAttr + 1);
        romantic = Random.Range(0, maxAttr + 1);
        chaotic = Random.Range(0, maxAttr + 1);
        // Debug.Log("Cozy: " + cozy + " Romantic: " + romantic + " Chaotic: " + chaotic);
    }

    public void generateReview() {
        // Customer gives better reviews the better their preferences are catered to
        Dictionary<string, float> preferences_dict = new Dictionary<string, float>();

        preferences_dict.Add("cozy", 20);
        preferences_dict.Add("romantic", 40);
        preferences_dict.Add("chaotic", 70);

        double modifier = 0;
        double cozyScore = 0;
        double romanticScore = 0;
        double chaoticScore = 0;
        string review = "";
        foreach (KeyValuePair<string, float> kvp in preferences_dict) {
            if (kvp.Value <= 20f) {
                // Hates
                modifier = -0.25;
            }
            else if (20f < kvp.Value && kvp.Value <= 40f) {
                // Dislikes
                modifier = -0.5;
            }
            else if (40f < kvp.Value && kvp.Value <= 60f) {
                // Doesn't care
                modifier = 1;
            }
            else if (60f < kvp.Value && kvp.Value <= 80f) {
                // Likes
                modifier = 1.25;
            }
            else if (80f < kvp.Value && kvp.Value <= 100f) {
                // Loves
                modifier = 1.5;
            }

            if (kvp.Key == "cozy") {
                cozyScore = GameManager.Instance.cozy * modifier;
            }
            else if (kvp.Key == "romantic") {
                romanticScore = GameManager.Instance.romantic * modifier;
            }
            else if (kvp.Key == "chaotic") {
                chaoticScore = GameManager.Instance.chaotic * modifier;
            }
            else {
                Debug.Log("Trait not recognized");
            }
        }
        double totalScore = cozyScore + romanticScore + chaoticScore;
        // Debug.Log("cozy: " + cozyScore + " romantic: " + romanticScore + " chaotic: " + chaoticScore);
        // Debug.Log("Total Score: " + totalScore);

        // TODO: Needs a whole lot of balancing depending on assets and how simple/complex we would like to have the satisfaction measure.
        if (totalScore <= 25) {
            // Customer hated the club
            review = "The customer feels like they walked into a tin of sardines";
        }
        else if (25 < totalScore && totalScore <= 45) {
            // customer disliked the club
            review = "The customer thinks the drinks are crappy and the floors are sticky";
        }
        else if (45 < totalScore && totalScore <= 65) {
            // It was ok
            review = "The customer compares your club to a mediocre cup of coffee";
        }
        else if (65 < totalScore && totalScore <= 85) {
            // Liked it
            review = "The customer had a decent time jumping on the dance floor";
        }
        else if (85 < totalScore && totalScore <= 110) {
            // Loved it
            review = "The customer considers this one of the best clubs out there";
        }
        else if (totalScore > 110) {
            // Experience of a life time
            review = "The customer considers selling their first born to fund you";
        }
        else {
            Debug.Log("Review Generation Error");
        }

        GameManager.Instance.addReview(review);
    }
}
