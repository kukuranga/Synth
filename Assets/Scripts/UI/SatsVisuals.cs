using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatsVisuals : MonoBehaviour
{
    public GameObject _Button;
    public GameObject _LockedText;
    public Image _Image;

    private void Update()
    {
        if (GameManager.Instance.StatsUnlocked)
        {
            _Button.SetActive(true);
            _LockedText.SetActive(false);

            Color newColor = _Image.color;
            newColor.a = 1f;
            _Image.color = newColor;
        }
        else
        {
            _Button.SetActive(false);
            _LockedText.SetActive(true);

            Color newColor = _Image.color;
            newColor.a = 0.3f;
            _Image.color = newColor;
        }
    }
}
