using System;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    [Serializable]
    public class AudioTrack
    {
        public AudioClip clip;
        public string name;
    }
    
    public List<AudioTrack> tracks;
    
    public void PlayOnAllAudioSources(AudioClip clip)
    {
        var audioSources = GameObject.FindGameObjectsWithTag("AudioSources");
        foreach (var audioSource in audioSources)
        {
            var audioSourceComp = audioSource.GetComponent<AudioSource>(); 
            audioSourceComp.clip = clip;
            audioSourceComp.Play();
        }
    }
    
    public void PauseOnAllSources()
    {
        var audioSources = GameObject.FindGameObjectsWithTag("AudioSources");
        foreach (var audioSource in audioSources)
        {
            var audioSourceComp = audioSource.GetComponent<AudioSource>(); 
            audioSourceComp.Pause();
        }
    }
    
    public void UnPauseOnAllSources()
    {
        var audioSources = GameObject.FindGameObjectsWithTag("AudioSources");
        foreach (var audioSource in audioSources)
        {
            var audioSourceComp = audioSource.GetComponent<AudioSource>(); 
            audioSourceComp.UnPause();
        }
    }   
}