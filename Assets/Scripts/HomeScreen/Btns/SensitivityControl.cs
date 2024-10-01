using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityControl : MonoBehaviour
{
    public Slider _slider;

    private void Start()
    {
        // Get the slider component
        _slider = GetComponent<Slider>();

        // Set the slider's initial value to the current music volume
        _slider.value = GameManager.Instance._SwipeSensitivity;

        // Add a listener to the slider so that when the value changes, it updates the volume
        _slider.onValueChanged.AddListener(SetSensitivity);
    }

    // This method will be called when the slider value changes
    public void SetSensitivity(float val)
    {
        GameManager.Instance.SetSwipeSensitivity((int)val);
    }
}
