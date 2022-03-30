using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.PlayerLoop;
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
    public Minigames minigames;
    public int startHour;
    public int clubOpeningHour = 20;
    public int clubClosingHour = 5;
    private static Camera _camera;
    
    public AstarPath gridPath;
    public bool updatePath = false;
    
    private GameCycleState _gameCycleState = GameCycleState.None;

    public int GameClockHours => (int)(CurrentGameClock / 60);
    public int GameClockMinutes => (int)(CurrentGameClock % 60);
    public string GameClockString => $"{GameClockHours:00}:{GameClockMinutes:00}";
    public bool IsNightTime => GameClockHours >= clubOpeningHour || GameClockHours < clubClosingHour;
    // In-game clock in minutes
    public float CurrentGameClock = 0;

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

    public SliderScript satisfactionSlider;
    public TextMeshProUGUI nightText;
    public int night = 0;

    public GameObject nightScreen;
    public TextMeshProUGUI nightScreenText;
    public GameObject dayScreen;
    public TextMeshProUGUI dayScreenText;
    public float transitionTime = 1f;
    
    private void Start()
    {
        CurrentGameClock = startHour * 60;
        _camera = Camera.main;
        SetNight(night);
    }

    IEnumerator StartDayScreen()
    {
        dayScreenText.text = "Night: " + (night-1) + " Complete";
        dayScreen.SetActive(true);

        yield return new WaitForSeconds(transitionTime);

        dayScreen.SetActive(false);
    }
    
    IEnumerator StartNightScreen()
    {
        
        nightScreenText.text = "Night: " + night + " Starting";
        nightScreen.SetActive(true);
        
        yield return new WaitForSeconds(transitionTime);
        
        nightScreen.SetActive(false);
    }

    IEnumerator NextNight()
    {
        if (night != 1)
        {
            StartCoroutine(StartDayScreen());
            yield return new WaitForSeconds(transitionTime);
        }

        StartCoroutine(StartNightScreen());
    }
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void SetNight(int value)
    {
        nightText.text = value.ToString();
        night = value;
    }
    
    private void Update()
    {
        clockText.text = GameClockString;

        if (IsNightTime && _gameCycleState != GameCycleState.NightTime)
        {
            SetNight(++night);
            
            _setupForNightTime();
        }
        else if (!IsNightTime && _gameCycleState != GameCycleState.DayTime && customerManager._customers.Count == 0)
        {
            _setupForDayTime();
        }

        CurrentGameClock += Time.deltaTime;

        if (GameClockHours == 24)
        {
            CurrentGameClock = 0;
        }
    }

    private void _setupForNightTime()
    {
        lightsController.NightLightsEnabled = true;
        lightsController.SetLightShow(Lightshow.Lightshow1);
        _gameCycleState = GameCycleState.NightTime;

        StartCoroutine(NextNight());
    }

    private void _setupForDayTime()
    {
        lightsController.NightLightsEnabled = false;
        _gameCycleState = GameCycleState.DayTime;

        CurrentGameClock = startHour * 60;
    }

    public static Vector3 GetMouseTo2DWorldPos(Camera camera = null)
    {
        var mousePos = (camera ? camera : _camera).ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        return mousePos;
 
    }

    private void LateUpdate()
    {
        if (updatePath)
        {
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
