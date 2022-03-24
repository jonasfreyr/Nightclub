using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

internal enum GameCycleState
{
    None,
    NightTime,
    DayTime
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public LightsController lightsController;
    public GridManager gridManager;
    public GameObject draggingObject = null;
    public TextMeshProUGUI clockText;
    public AudioManager audioManager;
    public CustomerManager customerManager;
    public EmployeeBehaviour employee;
    public EventSystem eventSystem;
    public int startHour;
    public int clubOpeningHour = 20;
    public int clubClosingHour = 5;
    private static Camera _camera;
    
    public AstarPath gridPath;
    public bool updatePath = false;
    
    private GameCycleState _gameCycleState = GameCycleState.None;

    public int GameClockHours => CurrentGameClock / 60;
    public int GameClockMinutes => CurrentGameClock % 60;
    public string GameClockString => $"{GameClockHours:00}:{GameClockMinutes:00}";
    public bool IsNightTime => GameClockHours >= clubOpeningHour || GameClockHours < clubClosingHour;
    // In-game clock in minutes
    private int CurrentGameClock => ((int)Time.time + startHour * 60) % (24 * 60);

    public float cozy;
    public float romantic;
    public float chaotic;
    public float satisfaction;
    public GameObject reviewPrefab;
    public GameObject reviewPanel;
    public Scrollbar bar;
    public bool speakersBroken = false;
    public bool bathroomBroken = false;
    public bool barBroken = false;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        clockText.text = GameClockString;

        if (IsNightTime && _gameCycleState != GameCycleState.NightTime)
        {
            _setupForNightTime();
        }
        else if (!IsNightTime && _gameCycleState != GameCycleState.DayTime)
        {
            _setupForDayTime();
        }
    }

    private void _setupForNightTime()
    {
        lightsController.NightLightsEnabled = true;
        lightsController.SetLightShow(Lightshow.Lightshow1);
        _gameCycleState = GameCycleState.NightTime;
    }

    private void _setupForDayTime()
    {
        lightsController.NightLightsEnabled = false;
        _gameCycleState = GameCycleState.DayTime;
    }

    public static Vector3 GetMouseTo2DWorldPos()
    {
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        return mousePos;
 
    }

    private void LateUpdate()
    {


        if (updatePath)
        {
            Debug.Log("Yay");
            gridPath.Scan();
        }
            

        updatePath = false;
    }

    public void addReview(string review) {
        var newReview = Instantiate(reviewPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        newReview.transform.SetParent(reviewPanel.transform);
        TMPro.TextMeshProUGUI reviewText = newReview.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        reviewText.text = review;
    }
}
