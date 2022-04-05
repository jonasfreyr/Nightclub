using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public AudioMenu audioMenu;
    [SerializeField] GameObject pauseMenu;
    public bool IsPaused { get; private set; }

    // Start is called before the first frame update
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        GameManager.Instance.audioManager.PauseOnAllSources();
    }

    public void Resume()
    {
        if (audioMenu.IsOpen)
        {
            audioMenu.CloseMenu();
        }
        
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        GameManager.Instance.audioManager.UnPauseOnAllSources();
    }

    public void OpenAudioMenu()
    {
        pauseMenu.SetActive(false);
        audioMenu.gameObject.SetActive(true);
    }

    // Update is called once per frame
    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
}
