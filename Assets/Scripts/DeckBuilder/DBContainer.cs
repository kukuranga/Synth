using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DBContainer : MonoBehaviour
{
    public bool _Unlocked;
    public DBUnit _unit;
    public GameObject _BackGround;
    public GameObject _yellowresourceGO;
    public GameObject _GreenresourceGO;
    public GameObject _Star;
    public GameObject _Danger;
    public TextMeshPro _YellowText;
    public TextMeshPro _GreenText;
    public SpriteRenderer _spriteRender;

    private void Start()
    {
        DisableAllVisuals();
    }

    private void Update()
    {
        if (_unit == null || !_Unlocked)
        {
            DisableAllVisuals();
        }
        else //Remove this section 
        {
            if (_unit._YellowResourceGain == 0)
                _yellowresourceGO.SetActive(false);
            else
                _YellowText.text = _unit._YellowResourceGain.ToString();

            if (_unit._GreenResourceGain == 0)
                _GreenresourceGO.SetActive(false);
            else
                _GreenText.text = _unit._GreenResourceGain.ToString();

            if (_unit._Danger)
                _Danger.SetActive(true);
            if (_unit._Star)
                _Star.SetActive(true);
        }

        if (_Unlocked)
            _BackGround.SetActive(true);
        else
            _BackGround.SetActive(false);
    }

    public void SetUnit(DBUnit _u)
    {
        _unit = _u;
        _spriteRender.sprite = _u._sprite;

        if (_unit._YellowResourceGain == 0)
            _yellowresourceGO.SetActive(false);
        else
        {
            _yellowresourceGO.SetActive(true);
            _YellowText.text = _unit._YellowResourceGain.ToString();
        }
        if (_unit._GreenResourceGain == 0)
            _GreenresourceGO.SetActive(false);
        else
        {
            _GreenresourceGO.SetActive(true);
            _GreenText.text = _unit._GreenResourceGain.ToString();
        }
        if (_unit._Danger)
            _Danger.SetActive(true);
        if (_unit._Star)
            _Star.SetActive(true);
    }

    public void DisableAllVisuals()
    {
        _Danger.SetActive(false);
        _Star.SetActive(false);
        _yellowresourceGO.SetActive(false);
        _GreenresourceGO.SetActive(false);
        _spriteRender.sprite = null;
    }

    public void ClearContainer()
    {
        DisableAllVisuals();
        _unit = null;
        _Unlocked = false;
    }
}
