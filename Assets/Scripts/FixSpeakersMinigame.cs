using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixSpeakersMinigame : MonoBehaviour
{
    public float winningTimer = 0.5f;
    // public ToiletPlunger toiletPlunger;
    public bool HasWon;
    private bool _hasWinningTimeCompleted;
    private bool _startedWinningtimeCounter;

    // private void Update()
    // {
    //     if (toiletPlunger.Counter >= 3 && !_startedWinningtimeCounter && !_hasWinningTimeCompleted)
    //     {
    //         _startedWinningtimeCounter = true;
    //         StartCoroutine(_startWinningProcess());
    //     } 
    // }

    // private IEnumerator _startWinningProcess()
    // {
    //     yield return new WaitForSeconds(winningTimer);
    //     _hasWinningTimeCompleted = true;
    //     _startedWinningtimeCounter = false;
    // }

    public void ResetGame()
    {
        _hasWinningTimeCompleted = false;
        _startedWinningtimeCounter = false;
    }
}
