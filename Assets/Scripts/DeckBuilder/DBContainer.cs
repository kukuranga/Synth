using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DBContainer : MonoBehaviour , IPointerClickHandler
{
    public bool _Shop;
    public bool _Unlocked;
    public DBUnit _unit;
    public GameObject _BackGround;
    public GameObject _yellowresourceGO;
    public GameObject _GreenresourceGO;
    public GameObject _Star;
    public GameObject _Danger;
    public SpriteRenderer _Ability;
    public TextMeshPro _YellowText;
    public TextMeshPro _GreenText;
    public SpriteRenderer _spriteRender;
    public Sprite _FlagSprite;
    public bool _Activated;

    private Quaternion originalRotation;

    private void Start()
    {
        DisableAllVisuals();
        originalRotation = transform.rotation;
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

    void LateUpdate()
    {
        // Reset rotation after parent rotates
        transform.rotation = originalRotation;
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

        if (_unit._FlagDangerReduction)
            _Ability.sprite = _FlagSprite;
        else if(_unit._Ability != null)
        {
            _Ability.gameObject.SetActive(true);
            _Ability.sprite = _unit._Ability._sprite;
        }
        else
        {
            _Ability.gameObject.SetActive(false);
        }
    }

    public void DisableAllVisuals()
    {
        _Danger.SetActive(false);
        _Star.SetActive(false);
        _yellowresourceGO.SetActive(false);
        _GreenresourceGO.SetActive(false);
        _spriteRender.sprite = null;
        _Ability.sprite = null;
    }

    public void ClearContainer()
    {
        DisableAllVisuals();
        _unit = null;
        _Unlocked = false;
        _Activated = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //onlick will display the information on the ability and set the game state to selecting an option
        //the options will change how this onclick works if the gamestate is set to select a unit
        //a method to change the state back should also be made

        switch(Game2Manager.Instance._gameState)
        {
            case GameState.GamePlay:
                    if (_unit == null)
                        return;

                    //Show Definintion of red values here
                    if (_unit._Danger)
                        DBMessageManager.Instance.UpdateMessage("Unit Contains Danger");

                    if (_unit._Ability == null)
                        return;


                    if (_Activated && !_Shop)
                    {
                        //the effects of the activated ability will trigger here.
                        _Activated = false;
                        _unit._Ability.ActivateAbility();
                    }
                break;

            case GameState.SelectUnit:
                if (Game2Manager.Instance._TempUnit != this._unit)
                    Game2Manager.Instance.UnitClicked(this);
                else
                    DBMessageManager.Instance.UpdateMessage("Cant Select the same unit");
                break;
        }

        
    }
}
