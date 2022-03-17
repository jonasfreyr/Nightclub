using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{
    public Transform trackListItemPrefab;
    public Transform trackList;

    private void Start()
    {
        var tracks = GameManager.Instance.audioManager.tracks;

        foreach (var (track, i) in tracks.Select((v, i) => (v, i)))
        {
            _addTrackListItem(track, i);
        }
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void SetTrack(AudioClip track)
    {
        GameManager.Instance.audioManager.PlayOnAllAudioSources(track);
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
