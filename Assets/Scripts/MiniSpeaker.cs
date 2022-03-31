using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniSpeaker : MonoBehaviour, IPointerClickHandler
{
    
    Animator anim;
    public int hits;

    public AudioClip hit;
    private AudioSource _audioSource;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        // Debug.Log("Speaker clicked");
        anim.Play("SpeakerHit", -1, 0f);
        hits++;
        
        _audioSource.PlayOneShot(hit, 1f);
    }
}
