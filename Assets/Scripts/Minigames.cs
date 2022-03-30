
using System;
using UnityEngine;

public enum MinigameType
{
    FixPipes,
}

public class Minigames : MonoBehaviour
{
    public FixPipesMinigame fixPipesMinigame;
    
    public bool IsPlayingMinigame { get; private set; }
    public bool Succeeded { get; private set; }

    private MinigameType _currentMinigame;

    private void Start()
    {
        PlayMinigame(MinigameType.FixPipes);
    }

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
        }
    }

    public void PlayMinigame(MinigameType type)
    {
        switch (type)
        {
            case MinigameType.FixPipes:
                IsPlayingMinigame = true;
                _currentMinigame = type;
                gameObject.SetActive(true);
                fixPipesMinigame.gameObject.SetActive(true);
                fixPipesMinigame.ResetGame();
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