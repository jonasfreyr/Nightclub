using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerManager : MonoBehaviour
{
    
    public int maxCustomers;
    public float lowerEndTime;
    public float upperEndTime;

    public GameObject customerPrefab;
    
    [Serializable]
    public class customerBody
    {
        public Sprite body;
        public Sprite leftLeg;
        public Sprite rightLeg;
        
    }
    
    public customerBody[] customerSprites;

    public Transform[] spawnPoints;
    
    public PointOfInterest[] bars;
    public PointOfInterest[] danceFloors;
    public PointOfInterest[] bathrooms;
    public PointOfInterest[] commonAreas;
    
    private List<GameObject> _customers;
    private float _timeToNextCustomer;
    private float _lastSpawned;

    // Start is called before the first frame update
    void Start()
    {
        _customers = new List<GameObject>();
    }
    
    public Transform GetRandomSpawnpoint(Transform[] pois)
    {
        return pois[Random.Range(0, pois.Length)];
    }
    
    public PointOfInterest GetRandomPOI(PointOfInterest[] pois)
    {
        return pois[Random.Range(0, pois.Length)];
    }
    
    public Vector3 GetRandomPOIPosition(PointOfInterest[] pois)
    {
        var poi = GetRandomPOI(pois);

        return poi.GetRandomPointWithinCollider();
    }

    private void SetCustomerSprites(GameObject customer)
    {
        var customerSprite = customerSprites[Random.Range(0, customerSprites.Length)];
        
        var customerBody = customer.transform.GetChild(0);
        var customerLeftLeg = customerBody.transform.GetChild(0);
        var customerRightLeg = customerBody.transform.GetChild(1);
        
        var bodySpriteRenderer = customerBody.GetComponent<SpriteRenderer>();
        var leftLegSpriteRenderer = customerLeftLeg.GetComponent<SpriteRenderer>();
        var rightLegSpriteRenderer = customerRightLeg.GetComponent<SpriteRenderer>();
        
        bodySpriteRenderer.sprite = customerSprite.body;
        leftLegSpriteRenderer.sprite = customerSprite.leftLeg;
        rightLegSpriteRenderer.sprite = customerSprite.rightLeg;
    }
    
    void SpawnCustomer()
    {
        
        if (!GameManager.Instance.IsNightTime) return;
        
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        var customer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity, transform);

        SetCustomerSprites(customer);
        
        var customerScript = customer.GetComponent<CustomerBehaviour>();
        customerScript.customerManager = this;

        customerScript.thirst = Random.Range(0f, 100f);
        customerScript.funky = Random.Range(0f, 100f);
        customerScript.mictury = Random.Range(0f, 100f);
        customerScript.pukeness = Random.Range(0f, 20f);

        customerScript.thirstThreshold = Random.Range(60f, 100f);
        customerScript.funkThreshold = Random.Range(60f, 100f);
        customerScript.micturyThreshold = Random.Range(60f, 100f);
        customerScript.pukeThreshold = Random.Range(80f, 100f);
        
        _customers.Add(customer);
    }
    
    // Update is called once per frame
    void Update()
    {   
        
        if (_customers.Count >= maxCustomers)
        {
            _lastSpawned = Time.time;
            
            return;
        }

        if (Time.time >= _lastSpawned + _timeToNextCustomer)
        {
            // SpawnCustomer();
            _lastSpawned = Time.time;

            _timeToNextCustomer = Random.Range(lowerEndTime, upperEndTime);
        }
    }
}
