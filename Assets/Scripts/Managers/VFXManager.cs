using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VFXManager : Singleton<VFXManager>
{

    public Image _LevelImage;
    public Image _MovesImage;
    public ParticleSystem _RainPS;
    public GameObject _VFXRain;
    public GameObject _VFXDust;
    public GameObject _VFXParticles;
    public GameObject _VFXSynthBackground;

    //TODO: Have different presets of items to spawn with a unique background


    private void Start()
    {
        _VFXDust.SetActive(false);
        _VFXParticles.SetActive(false);
        _VFXRain.SetActive(true);
        _VFXSynthBackground.SetActive(true);

        OverwierManager.Instance.FadeIn(Color.cyan, 0.2f, 0.1f);
    }

    private bool _InUse = false;

    public void FadeInLevel(Color _c, float _Alpha, float duration)
    {
        if (!_InUse)
        {
            _InUse = true;
            StartCoroutine(Fade(_c, _Alpha, duration, _LevelImage));
        }
    }

    public void FadeInMoves(Color _c, float _Alpha, float duration)
    {
        if (!_InUse)
        {
            _InUse = true;
            StartCoroutine(Fade(_c, _Alpha, duration, _MovesImage));
        }
    }

    #region Rain

    public void RainEnable()
    {
        _RainPS.gameObject.SetActive(true);
    }

    public void RainDisable()
    {
        _RainPS?.gameObject.SetActive(false);
    }

    #endregion

    IEnumerator Fade(Color _col, float _Alpha, float duration, Image _Image)
    {
        Color startColor = _Image.color;
        float elapsedTime = 0f;
        Color _c = _col;
        _c.a = _Alpha;

        while (elapsedTime < duration)
        {
            _Image.color = Color.Lerp(startColor, _c, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _Image.color = _c; // Ensure we reach the exact target color

        elapsedTime = 0;

        while (elapsedTime < (duration/1.5f))
        {
            _Image.color = Color.Lerp(_c, startColor, elapsedTime / (duration/1.5f));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _Image.color = startColor;

        _InUse = false;
        yield return null;
    }
}
