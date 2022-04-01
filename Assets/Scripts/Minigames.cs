
using System;
using UnityEngine;

public enum MinigameType
{
    FixPipes,
    UnclogToilet,
    FixSpeakers,
    BarGame,
    MoveBoxes,
}

public class Minigames : MonoBehaviour
{
    public FixPipesMinigame fixPipesMinigame;
    public UnclogToiletMinigame unclogToiletMinigame;
    public BarStockMinigame barStockMinigame;
    public MoveBoxesMinigame moveBoxesMinigame;
    public HitTheSpeaker fixSpeakersMinigame;
    
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
                    GameManager.Instance.AddSatisfaction(5);
                }
                break;
            case MinigameType.UnclogToilet:
                if (unclogToiletMinigame.HasWon)
                {
                    IsPlayingMinigame = false;
                    Succeeded = true;
                    unclogToiletMinigame.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                    GameManager.Instance.AddSatisfaction(5);
                }
                break;
            case MinigameType.BarGame:
                if (barStockMinigame.HasWon)
                {
                    IsPlayingMinigame = false;
                    Succeeded = true;
                    barStockMinigame.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                    GameManager.Instance.AddSatisfaction(5);
                }
                break;
            case MinigameType.MoveBoxes:
                if (moveBoxesMinigame.HasWon)
                {
                    IsPlayingMinigame = false;
                    Succeeded = true;
                    moveBoxesMinigame.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                    GameManager.Instance.AddSatisfaction(5);
                }
                break;
            case MinigameType.FixSpeakers:
                if (fixSpeakersMinigame.HasWon) {
                    IsPlayingMinigame = false;
                    Succeeded = true;
                    fixSpeakersMinigame.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                    GameManager.Instance.AddSatisfaction(5);
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
            case MinigameType.BarGame:
                IsPlayingMinigame = true;
                _currentMinigame = MinigameType.BarGame;
                gameObject.SetActive(true);
                barStockMinigame.gameObject.SetActive(true);
                barStockMinigame.ResetGame();
                break;
            case MinigameType.MoveBoxes:
                IsPlayingMinigame = true;
                _currentMinigame = MinigameType.MoveBoxes;
                gameObject.SetActive(true);
                moveBoxesMinigame.gameObject.SetActive(true);
                moveBoxesMinigame.ResetGame();
                break;
            case MinigameType.FixSpeakers:
                IsPlayingMinigame = true;
                _currentMinigame = MinigameType.FixSpeakers;
                gameObject.SetActive(true);
                fixSpeakersMinigame.gameObject.SetActive(true);
                fixSpeakersMinigame.ResetGame();
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