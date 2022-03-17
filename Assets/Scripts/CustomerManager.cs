using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    
    public int maxCustomers;
    public float lowerEndTime;
    public float upperEndTime;

    public GameObject customerPrefab;
    public Sprite[] customerSprites;

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

    public PointOfInterest GetRandomPOI(PointOfInterest[] pois)
    {
        return pois[Random.Range(0, pois.Length)];
    }
    
    public Vector3 GetRandomPOIPosition(PointOfInterest[] pois)
    {
        var poi = GetRandomPOI(pois);

        return poi.GetRandomPointWithinCollider();
    }
    
    void SpawnCustomer()
    {
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var customerSprite = customerSprites[Random.Range(0, customerSprites.Length)];

        var customer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity, transform);
        
        var customerGraphics = customer.transform.GetChild(0);
        var customersSpriteRenderer = customerGraphics.GetComponent<SpriteRenderer>();
        customersSpriteRenderer.sprite = customerSprite;

        var customerScript = customer.GetComponent<CustomerBehaviour>();
        customerScript.customerManager = this;
        
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
            SpawnCustomer();
            _lastSpawned = Time.time;

            _timeToNextCustomer = Random.Range(lowerEndTime, upperEndTime);
        }
    }
}
