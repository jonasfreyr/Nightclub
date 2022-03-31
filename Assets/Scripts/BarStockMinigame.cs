using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarStockMinigame : MonoBehaviour
{
    public bool HasWon => tiles.All(tile => tile.placed) && _hasWinningTimerCompleted;
    public float winningTimer = 0.5f;
    private bool _hasWinningTimerCompleted;
    private bool _startedWinningtimerCounter;

    public BottleTile[] tiles;
    
    public BottleTile GetTile(BoxCollider2D _collider)
    {
        foreach (var tile in tiles)
        {
            if (_collider.IsTouching(tile.collider) && !tile.placed) return tile;
        }

        return null;
    }

    public void Update()
    {
        
        if (tiles.All(tile => tile.placed) && !_startedWinningtimerCounter && !_hasWinningTimerCompleted)
        {
            _startedWinningtimerCounter = true;
            StartCoroutine(_startWinningProcess());
        }
    }

    public void ResetGame()
    {
        _hasWinningTimerCompleted = false;
        _startedWinningtimerCounter = false;

        foreach (var tile in tiles)
        {
            tile.placed = false;
        }

        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("BeerBottle")) Destroy(child.gameObject);
        }
        
    }
    
    private IEnumerator _startWinningProcess()
    {
        yield return new WaitForSeconds(winningTimer);
        _hasWinningTimerCompleted = true;
    }
}
