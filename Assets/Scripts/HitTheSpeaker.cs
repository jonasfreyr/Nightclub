using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTheSpeaker : MonoBehaviour
{
    public float winningTimer = 0.5f;
    public MiniSpeaker speaker;
    private int hitsRequired;
    
    public bool HasWon => speaker.hits >= hitsRequired;
    private bool _hasWinningTimeCompleted;
    private bool _startedWinningtimeCounter;

    void Start()
    {
        hitsRequired = Random.Range(5, 10);
    }

    void Update() {
        if (speaker.hits >= hitsRequired) {
            _startedWinningtimeCounter = true;
            StartCoroutine(_startWinningProcess());
        }
    }

    private IEnumerator _startWinningProcess() {
        yield return new WaitForSeconds(winningTimer);
        _hasWinningTimeCompleted = true;
        _startedWinningtimeCounter = false;
    }

    public void ResetGame() {
        speaker.hits = 0;
        hitsRequired = Random.Range(5, 10);
        _hasWinningTimeCompleted = false;
        _startedWinningtimeCounter = false;
    }
}
