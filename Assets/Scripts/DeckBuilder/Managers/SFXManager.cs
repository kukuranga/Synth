using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{

    public AudioSource audioSource;
    public List<AudioClip> audioClips;

    private bool isMuted = false;

    public void PlaySound(string clipName)
    {
        AudioClip clip = audioClips.Find(c => c.name == clipName);

        if (clip != null)
        {
            // Check if the audio source is already playing this specific clip
            if (!audioSource.isPlaying || audioSource.clip != clip)
            {
                audioSource.clip = clip; // Set the clip (optional, depending on your setup)
                audioSource.PlayOneShot(clip); // Play the sound
            }
            else
            {
                Debug.Log("Clip is already playing: " + clipName);
            }
        }
        else
        {
            Debug.LogWarning("Audio clip not found: " + clipName);
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp(volume, 0f, 1f);
    }

    public float GetFXVolume()
    {
        return audioSource.volume;
    }

    public void SetFXVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }

    public void Mute()
    {
        isMuted = true;
        audioSource.mute = true;
    }

    public void Unmute()
    {
        isMuted = false;
        audioSource.mute = false;
    }

    public bool IsMuted()
    {
        return isMuted;
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void PauseSound()
    {
        audioSource.Pause();
    }

    public void UnpauseSound()
    {
        audioSource.UnPause();
    }
}
