using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnclogToiletMinigame : MonoBehaviour
{
    public float winningTimer = 0.5f;
    public ToiletPlunger toiletPlunger;
    public bool HasWon => toiletPlunger.Counter >= 3 && _hasWinningTimeCompleted;
    private int plungesRequired;
    private bool _hasWinningTimeCompleted;
    private bool _startedWinningtimeCounter;
    public TMPro.TextMeshProUGUI plungesLabel;


    private void Start() {
        plungesRequired = UnityEngine.Random.Range(3, 7);
        // Debug.Log("Plunges Required: " + plungesRequired);
    }

    private void Update()
    {
        if (plungesRequired - toiletPlunger.Counter >= 0) {
            plungesLabel.text = (plungesRequired - toiletPlunger.Counter).ToString();
        }

        if (toiletPlunger.Counter >= plungesRequired && !_startedWinningtimeCounter && !_hasWinningTimeCompleted)
        {
            _startedWinningtimeCounter = true;
            StartCoroutine(_startWinningProcess());
        } 
    }

    private IEnumerator _startWinningProcess()
    {
        yield return new WaitForSeconds(winningTimer);
        _hasWinningTimeCompleted = true;
        _startedWinningtimeCounter = false;
    }

    public void ResetGame()
    {
        toiletPlunger.ResetPlunger();
        plungesRequired = UnityEngine.Random.Range(3, 7);
        _hasWinningTimeCompleted = false;
        _startedWinningtimeCounter = false;
    }
}