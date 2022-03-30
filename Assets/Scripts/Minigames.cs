﻿
using System;
using UnityEngine;

public enum MinigameType
{
    FixPipes,
    UnclogToilet,
}

public class Minigames : MonoBehaviour
{
    public FixPipesMinigame fixPipesMinigame;
    public UnclogToiletMinigame unclogToiletMinigame;
    
    public bool IsPlayingMinigame { get; private set; }
    public bool Succeeded { get; private set; }

    private MinigameType _currentMinigame;
    
    private void Update()
    {
        if (!IsPlayingMinigame) return;
        
        switch (_currentMinigame)
        {
            case MinigameType.FixPipes:
                if (fixPipesMinigame.HasWon)
                {
                    IsPlayingMinigame = false;
                    Succeeded = true;
                    fixPipesMinigame.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                }
                break;
            case MinigameType.UnclogToilet:
                if (unclogToiletMinigame.HasWon)
                {
                    IsPlayingMinigame = false;
                    Succeeded = true;
                    unclogToiletMinigame.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                }
                break;
        }
    }

    public void PlayMinigame(MinigameType type)
    {
        switch (type)
        {
            case MinigameType.FixPipes:
                IsPlayingMinigame = true;
                _currentMinigame = MinigameType.FixPipes;
                gameObject.SetActive(true);
                fixPipesMinigame.gameObject.SetActive(true);
                fixPipesMinigame.ResetGame();
                break;
            case MinigameType.UnclogToilet:
                IsPlayingMinigame = true;
                _currentMinigame = MinigameType.UnclogToilet;
                gameObject.SetActive(true);
                unclogToiletMinigame.gameObject.SetActive(true);
                unclogToiletMinigame.ResetGame();
                break;
        }
    }

    public void CancelMinigame()
    {
        IsPlayingMinigame = false;
        Succeeded = false;
        gameObject.SetActive(false);
    }
}