using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VFXManager : Singleton<VFXManager>
{

    public Image _LevelImage;
    public Image _MovesImage;
    public GameObject _SynthBackground;
    public ParticleSystem _RainPS;
    public GameObject _VFXRain;
    public GameObject _VFXDust;
    public GameObject _VFXParticles;
    public GameObject _VFXSynthBackground;
    public Color _BaseBackgroundColor;
    public Color _DustBackgroundColor;
    public Color _RainBackgroundColor;
    public Color _GoldBackgroundColor;
    public Color _RedBackgroundColor;

    private void Start()
    {
        _VFXDust.SetActive(false);
        _VFXParticles.SetActive(false);
        _VFXRain.SetActive(false);
        _VFXSynthBackground.SetActive(true);

        //Figure out how the acctivated backgrounds will work with gameplay

        if(GameManager.Instance._levelPreSet != LevelPreSet.Normal)
        {
            //_SynthBackground.SetActive(false);
        }

        switch(GameManager.Instance._levelPreSet)
        {
            case LevelPreSet.Normal:
                _VFXDust.SetActive(false);
                _VFXParticles.SetActive(false);
                _VFXRain.SetActive(false);
                _VFXSynthBackground.SetActive(false);
                OverwierManager.Instance._MainCamera.backgroundColor = _BaseBackgroundColor;
                break;
            case LevelPreSet.Dust:
                _VFXDust.SetActive(true);
                _VFXParticles.SetActive(false);
                _VFXRain.SetActive(false);
                _VFXSynthBackground.SetActive(false);
                OverwierManager.Instance._MainCamera.backgroundColor = _DustBackgroundColor;
                break;
            case LevelPreSet.gold:
                _VFXDust.SetActive(false);
                _VFXParticles.SetActive(false);
                _VFXRain.SetActive(false);
                _VFXSynthBackground.SetActive(false);
                OverwierManager.Instance._MainCamera.backgroundColor = _GoldBackgroundColor;
                break;
            case LevelPreSet.lava:
                _VFXDust.SetActive(false);
                _VFXParticles.SetActive(true);
                _VFXRain.SetActive(false);
                _VFXSynthBackground.SetActive(false);
                OverwierManager.Instance._MainCamera.backgroundColor = _RedBackgroundColor;
                break;
            case LevelPreSet.Rain:
                _VFXDust.SetActive(false);
                _VFXParticles.SetActive(false);
                _VFXRain.SetActive(true);
                _VFXSynthBackground.SetActive(false);
                OverwierManager.Instance._MainCamera.backgroundColor = _RainBackgroundColor;
                break;
        }
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
