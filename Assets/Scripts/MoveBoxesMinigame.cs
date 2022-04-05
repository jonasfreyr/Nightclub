using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class MoveBoxesMinigame : MonoBehaviour
{
    public float winningTimer = 0.5f;
    public RectTransform shipmenu1;
    public RectTransform shipmenu2;
    public RectTransform shelf1DestinationStart;
    public RectTransform shelf1DestinationEnd;
    public RectTransform shelf2DestinationStart;
    public RectTransform shelf2DestinationEnd;
    public RectTransform package1Marking;
    public RectTransform package2Marking;
    private Vector3 package1Destination;
    private Vector3 package2Destination;

    public bool HasWon => placed1 &&
                          placed2 &&
                          _hasWinningTimeCompleted;

    private bool _hasWinningTimeCompleted;
    private bool _startedWinningtimeCounter;
    private Vector3? shipment1InitialPosition;
    private Vector3? shipment2InitialPosition;
    private float? _shelf1MarkingPosition;
    private float? _shelf2MarkingPosition;

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
        if (Vector2.Distance(shipmenu1.position, package1Destination) < 20)
        {
            shipmenu1.position = package1Destination;
            shipmenu1.GetComponent<DraggableUIElement>().allowDragging = false;

            if (!placed1)
            {
                _audioSource.PlayOneShot(crateRattle, 1f);
                
                placed1 = true;
            }
            
        }
        
        if (Vector2.Distance(shipmenu2.position, package2Destination) < 20)
        {
            shipmenu2.position = package2Destination;
            shipmenu2.GetComponent<DraggableUIElement>().allowDragging = false;

            if (!placed2)
            {
                _audioSource.PlayOneShot(crateRattle, 1f);
                
                placed2 = true;
            }
            
        }

        if (placed1 &&
            placed2 &&
            !_startedWinningtimeCounter)
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

        if (_shelf1MarkingPosition == null)
        {
            _shelf1MarkingPosition = package1Marking.transform.position.y;
            _shelf2MarkingPosition = package2Marking.transform.position.y;
        }
        
        shipmenu1.GetComponent<DraggableUIElement>().allowDragging = true;
        shipmenu2.GetComponent<DraggableUIElement>().allowDragging = true;

        _hasWinningTimeCompleted = false;
        _startedWinningtimeCounter = false;
        placed1 = false;
        placed2 = false;
        
        var orderInShelf = UnityEngine.Random.Range(0, 2);
        
        // Decide blue package position
        var bluePackageDestX = UnityEngine.Random.Range(shelf1DestinationStart.position.x, shelf1DestinationEnd.position.x);
        package1Marking.position = new Vector2(bluePackageDestX, orderInShelf == 0 ? _shelf1MarkingPosition.Value : _shelf2MarkingPosition.Value);
        package1Destination = new Vector3(bluePackageDestX, orderInShelf == 0 ? shelf1DestinationStart.position.y : shelf2DestinationStart.position.y);
        
        // Decide orange package position
        var orangePackageDestX = UnityEngine.Random.Range(shelf2DestinationStart.position.x, shelf2DestinationEnd.position.x);
        package2Marking.position = new Vector2(orangePackageDestX, orderInShelf == 1 ? _shelf1MarkingPosition.Value : _shelf2MarkingPosition.Value);
        package2Destination = new Vector3(orangePackageDestX, orderInShelf == 1 ? shelf1DestinationStart.position.y : shelf2DestinationStart.position.y);
    }
}