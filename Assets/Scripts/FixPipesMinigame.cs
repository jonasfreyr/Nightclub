using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

public class FixPipesMinigame : MonoBehaviour
{
    public float winningTimer = 0.5f;
    public RectTransform trashcanTransform;
    public RectTransform brokenPipe1;
    public RectTransform brokenPipe2;
    public RectTransform replacementPipe;
    public RectTransform replacementPipePosition;
    public Trashcan trashcan;

    public bool HasWon => _hasThrownAwayBrokenPipe1 && _hasThrownAwayBrokenPipe2 && _hasReplacedPipe && _hasWinningTimerCompleted;
    private DraggableUIElement _replacementDragging;
    private bool _hasThrownAwayBrokenPipe1;
    private bool _hasThrownAwayBrokenPipe2;
    private bool _hasReplacedPipe;
    private bool _hasWinningTimerCompleted;
    private Vector2? _replacementPipeInitialPosition;
    private Vector2? _brokenPipe1InitialPosition;
    private Vector2? _brokenPipe2InitialPosition;

    private void Update()
    {
        if (!_hasThrownAwayBrokenPipe1)
        {
            if (Vector2.Distance(brokenPipe1.position, trashcanTransform.position) < 20)
            {
                _hasThrownAwayBrokenPipe1 = true;
                brokenPipe1.gameObject.SetActive(false);
            }
        }

        if (!_hasThrownAwayBrokenPipe2)
        {
            if (Vector2.Distance(brokenPipe2.position, trashcanTransform.position) < 20)
            {
                _hasThrownAwayBrokenPipe2 = true;
                brokenPipe2.gameObject.SetActive(false);
            }
        }

        // Forward event from trashcan
        if (!trashcan.IsDragging)
        {
            if (!_hasThrownAwayBrokenPipe1 && Vector2.Distance(brokenPipe1.position, trashcanTransform.position) < 60)
            {
                trashcan.forwardEventsToElement = brokenPipe1.GetComponent<DraggableUIElement>();
            }
            else if (!_hasThrownAwayBrokenPipe2 && Vector2.Distance(brokenPipe2.position, trashcanTransform.position) < 60)
            {
                trashcan.forwardEventsToElement = brokenPipe2.GetComponent<DraggableUIElement>();
            }
            else
            {
                trashcan.forwardEventsToElement = null;
            }   
        }

        if (_hasThrownAwayBrokenPipe1 && _hasThrownAwayBrokenPipe2 && !_hasReplacedPipe && !_replacementDragging.allowDragging)
        {
            _replacementDragging.allowDragging = true;
        }

        if (!_hasReplacedPipe)
        {
            if (Vector2.Distance(replacementPipe.position, replacementPipePosition.position) < 20)
            {
                _hasReplacedPipe = true;
                
                var position = replacementPipePosition.position;
                replacementPipe.position = position;
                _replacementDragging.allowDragging = false;

                StartCoroutine(_startWinningProcess());
            }
        }
    }

    public void ResetGame()
    {
        _replacementDragging = replacementPipe.gameObject.GetComponent<DraggableUIElement>();
        _replacementDragging.allowDragging = false;

        // On first call to ResetGame, the positions are correct, so we store those positions
        if (_replacementPipeInitialPosition == null)
        {
            _replacementPipeInitialPosition = replacementPipe.anchoredPosition;
            _brokenPipe1InitialPosition = brokenPipe1.anchoredPosition;
            _brokenPipe2InitialPosition = brokenPipe2.anchoredPosition;
        }
        else
        {
            replacementPipe.anchoredPosition = _replacementPipeInitialPosition.Value;
            brokenPipe1.anchoredPosition = _brokenPipe1InitialPosition.Value;
            brokenPipe2.anchoredPosition = _brokenPipe2InitialPosition.Value;    
        }
        
        brokenPipe1.gameObject.SetActive(true);
        brokenPipe2.gameObject.SetActive(true);
        _hasThrownAwayBrokenPipe1 = false;
        _hasThrownAwayBrokenPipe2 = false;
        _hasReplacedPipe = false;
        _hasWinningTimerCompleted = false;
    }

    private IEnumerator _startWinningProcess()
    {
        yield return new WaitForSeconds(winningTimer);
        _hasWinningTimerCompleted = true;
    }
}
