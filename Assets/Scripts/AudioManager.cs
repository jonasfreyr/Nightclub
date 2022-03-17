using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void PlayOnAllAudioSources(AudioClip clip)
    {
        var audioSources = GameObject.FindGameObjectsWithTag("AudioSources");
        foreach (var audioSource in audioSources)
        {
            audioSource.GetComponent<AudioSource>().clip = clip;
        }
    }
}