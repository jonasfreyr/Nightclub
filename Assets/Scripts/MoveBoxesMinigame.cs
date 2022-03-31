using System;
using System.Collections;
using UnityEngine;

public class MoveBoxesMinigame : MonoBehaviour
{
    public float winningTimer = 0.5f;
    public RectTransform shipmenu1;
    public RectTransform shipmenu2;
    public RectTransform shipmenu1destination;
    public RectTransform shipmenu2destination;

    public bool HasWon => shipmenu1.position == shipmenu1destination.position &&
                          shipmenu2.position == shipmenu2destination.position &&
                          _hasWinningTimeCompleted;

    private bool _hasWinningTimeCompleted;
    private bool _startedWinningtimeCounter;
    private Vector3? shipment1InitialPosition;
    private Vector3? shipment2InitialPosition;

    private bool placed1 = false;
    private bool placed2 = false;

    private AudioSource _audioSource;
    public AudioClip crateRattle;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Vector2.Distance(shipmenu1.position, shipmenu1destination.position) < 20)
        {
            shipmenu1.position = shipmenu1destination.position;
            shipmenu1.GetComponent<DraggableUIElement>().allowDragging = false;

            if (!placed1)
            {
                _audioSource.PlayOneShot(crateRattle, 1f);
                
                placed1 = true;
            }
            
        }
        
        if (Vector2.Distance(shipmenu2.position, shipmenu2destination.position) < 20)
        {
            shipmenu2.position = shipmenu2destination.position;
            shipmenu2.GetComponent<DraggableUIElement>().allowDragging = false;

            if (!placed2)
            {
                _audioSource.PlayOneShot(crateRattle, 1f);
                
                placed2 = true;
            }
            
        }

        if (shipmenu1.position == shipmenu1destination.position &&
            shipmenu2.position == shipmenu2destination.position)
        {
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
        if (shipment1InitialPosition == null)
        {
            shipment1InitialPosition = shipmenu1.position;
            shipment2InitialPosition = shipmenu2.position;
        }
        else
        {
            shipmenu1.position = shipment1InitialPosition.Value;
            shipmenu2.position = shipment2InitialPosition.Value;
        }
        
        shipmenu1.GetComponent<DraggableUIElement>().allowDragging = true;
        shipmenu2.GetComponent<DraggableUIElement>().allowDragging = true;

        _hasWinningTimeCompleted = false;
        _startedWinningtimeCounter = false;
        placed1 = false;
        placed2 = false;
    }
}