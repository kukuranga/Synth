using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverwierManager : Singleton<OverwierManager>
{
    public Image _Image;
    public Camera _MainCamera;
    private Color _MainBackgroundColor;

    private bool _InUse = false;
    private bool _CamInUse = false;

    private void Start()
    {
        var tempColor = _Image.color;
        tempColor = Color.white;
        tempColor.a = 0f;
        _Image.color = tempColor;
        _MainBackgroundColor = _MainCamera.backgroundColor;
    }

    public void ChangeBackgroundColor(Color _c)
    {
        _MainCamera.backgroundColor = _c;
    }

    public void FadeIn(Color _c , float _Alpha, float duration)
    {
        if(!_InUse)
        {
            _InUse = true;
            StartCoroutine(Fade(_c, _Alpha, duration));
        }
    }

    public void CameraFadeIn(Color _c, float duration)
    {
        if(!_CamInUse)
        {
            _CamInUse = true;
            StartCoroutine(CamFade(_c, duration));
        }
    }

    IEnumerator Fade(Color _col, float _Alpha, float duration)
    {
        Color startColor = _Image.color;
        float elapsedTime = 0f;
        Color _c = _col;
        _c.a =  _Alpha;

        while (elapsedTime < duration)
        {

            //Figure out the problem with the main camera not lerping at the coreect time
            _Image.color = Color.Lerp(startColor, _c, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _Image.color = _c; // Ensure we reach the exact target color

        elapsedTime = 0;

        while (elapsedTime < (duration/1.5f))
        {
            _Image.color = Color.Lerp( _c, startColor, elapsedTime / (duration/1.5f));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _Image.color = startColor;

        _InUse = false;
        yield return null;
    }

    IEnumerator CamFade(Color _col, float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            // Increment the time elapsed by the time since the last frame
            timeElapsed += Time.deltaTime;

            // Calculate the lerp factor
            float lerpFactor = timeElapsed / duration;

            // Lerp the camera's background color
            _MainCamera.backgroundColor = Color.Lerp(_MainBackgroundColor, _col, lerpFactor);

            // Wait for the next frame before continuing the loop
            yield return null;
        }

        // Ensure the color is set to the target color at the end of the transition
        _MainCamera.backgroundColor = _col;
        timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed +=Time.deltaTime;
            float lerpFactor = timeElapsed / duration;

            _MainCamera.backgroundColor = Color.Lerp(_col, _MainBackgroundColor, lerpFactor);

            yield return null;
        }

        _MainCamera.backgroundColor = _MainBackgroundColor;
        _InUse = false;
        yield return null;

    }
}
