using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject mainAudioMenu;
    public GameObject changeTrackMenu;
    public Transform trackListItemPrefab;
    public Transform trackList;
    public Slider volumeSlider;
    public bool IsOpen => gameObject.activeSelf;

    private void Start()
    {
        var tracks = GameManager.Instance.audioManager.tracks;

        foreach (var (track, i) in tracks.Select((v, i) => (v, i)))
        {
            _addTrackListItem(track, i);
        }
        
        volumeSlider.value = AudioListener.volume;
    }

    public void OpenChangeTrackSubmenu()
    {
        mainAudioMenu.SetActive(false);
        changeTrackMenu.SetActive(true);
    }
    
    public void CloseChangeTrackSubmenu()
    {
        changeTrackMenu.SetActive(false);
        mainAudioMenu.SetActive(true);
    }

    public void OnVolumeChange()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void CloseMenu()
    {
        CloseChangeTrackSubmenu();
        gameObject.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void SetTrack(AudioClip track)
    {
        GameManager.Instance.audioManager.PlayOnAllAudioSources(track);
        GameManager.Instance.audioManager.PauseOnAllSources();
    }
    
    private void _addTrackListItem(AudioManager.AudioTrack track, int listPosition)
    {
        var item = Instantiate(trackListItemPrefab, trackList);
        item.Find("Name").GetComponent<TextMeshProUGUI>().text = track.name;
        item.Find("Genre").GetComponent<TextMeshProUGUI>().text = "Genre";
        item.Find("SetButton").GetComponent<Button>().onClick.AddListener(() => SetTrack(track.clip));

        var itemPositionY = -25.0f - (46.0f * listPosition);
        item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, itemPositionY);
    }
}
