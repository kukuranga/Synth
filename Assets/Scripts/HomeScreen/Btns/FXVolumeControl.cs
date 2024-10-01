using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FXVolumeControl : MonoBehaviour
{
    public Slider _slider;

    private void Start()
    {
        // Get the slider component
        _slider = GetComponent<Slider>();

        // Set the slider's initial value to the current music volume
        _slider.value = AudioManager.Instance.GetFXVolume();

        // Add a listener to the slider so that when the value changes, it updates the volume
        _slider.onValueChanged.AddListener(SetVolume);
    }

    // This method will be called when the slider value changes
    public void SetVolume(float volume)
    {
        // Call the AudioManager's SetMusicVolume method with the new slider value
        AudioManager.Instance.SetFXVolume(volume);
    }
}
