using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
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
    public GameObject arrowsContainer;
    public Camera camera;
    public RectTransform canvas;
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

    private float maxSatisfaction = 100;
    public float satisfaction = 75;
    public GameObject reviewPrefab;
    public GameObject reviewPanel;
    public Scrollbar bar;
    public bool speakersBroken = false;
    public bool bathroomBroken = false;
    public bool barBroken = false;
    public bool sinkBroken = false;
    public bool shipmentWaiting = false;
    public bool pukeOnFloor = false;
    public int reviewCount = 0;
    public float reviewSatisfactionSum = 100;

    public SliderScript satisfactionSlider;
    public TextMeshProUGUI nightText;
    public int night = 0;

    public GameObject nightScreen;
    public TextMeshProUGUI nightScreenText;
    public GameObject dayScreen;
    public TextMeshProUGUI dayScreenText;
    public float transitionTime = 1f;

    public GameObject GameOverScreen;
    public TextMeshProUGUI gameOverText;
    private bool gameOver;
    
    public float timeToFail = 30f;
    
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
        minigames.CancelMinigame();
        
        if (night != 1)
        {
            StartCoroutine(StartDayScreen());
            yield return new WaitForSeconds(transitionTime);
        }
        
        eventSystem.interval -= 2;
        
        if (eventSystem.interval < 5)
        {
            eventSystem.interval = 5;
        }
        timeToFail -= 2;
        if (timeToFail <= 10)
        {
            timeToFail = 10;
        }

        if (night == 3)
        {
            eventSystem.maxBreak++;
        }
        

        eventSystem._timer = 0f;
        eventSystem.FixAll();
        employee.ResetEmployee();

        StartCoroutine(StartNightScreen());
    }
    
    private void Awake()
    {
        Time.timeScale = 1f;
        Instance = this;
    }
    
    public void SetNight(int value)
    {
        nightText.text = value.ToString();
        night = value;
    }

    public void GameOver()
    {
        gameOverText.text = "Survived " + night + " nights";
        
        GameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void ResetGame()
    {
        var scene = SceneManager.GetActiveScene(); 
        Time.timeScale = 1f;
        gameOver = false;
        SceneManager.LoadScene(scene.name);
    }
    
    private void Update()
    {
        if (gameOver) return;

        satisfactionSlider.SetSliderValue(satisfaction);
        
        if (satisfaction <= 0)
        {
            gameOver = true;

            GameOver();
        }

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

        CurrentGameClock += Time.deltaTime * 3;

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

    public void AddSatisfaction(float toAdd) {
        float result = satisfaction + toAdd;
        if (result >= maxSatisfaction) {
            result = maxSatisfaction;
        }
        if (result < 0) {
            result = 0;
        }
        satisfaction = result;
    }
}
