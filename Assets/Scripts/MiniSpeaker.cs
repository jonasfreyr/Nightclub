using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniSpeaker : MonoBehaviour, IPointerClickHandler
{
    
    Animator anim;
    public int hits;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        // Debug.Log("Speaker clicked");
        anim.Play("SpeakerHit", -1, 0f);
        hits++;
    }
}
