using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

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
    public GameObject _Coin;

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

    public void UpdateSpriteVisuals()
    {
        _spriteRender.sprite = _unit._sprite;
        _Unlocked = true;
    }

    public void SetUnit(DBUnit _u)
    {
        _unit = _u;
        _spriteRender.sprite = _u._sprite;


        StartCoroutine(SetUnit());

    }

    public void SetUnitNoAnimation(DBUnit _u)
    {
        _unit = _u;
        _spriteRender.sprite = _u._sprite;
        _Coin.SetActive(true);
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
        else if (_unit._Ability != null)
        {
            _Ability.gameObject.SetActive(true);
            _Ability.sprite = _unit._Ability._sprite;
        }
        else
        {
            _Ability.gameObject.SetActive(false);
        }
    }

    IEnumerator SetUnit()
    {

        Game2Manager.Instance.UpdateGameState(GameState.Animation);

        _spriteRender.gameObject.transform.position = DBContainerManager.Instance._SpawnPoint.transform.position;

        //Sequence seq = DOTween.Sequence();

        _Coin.SetActive(true);

        VFX2Manager.Instance.CameraFollow(_spriteRender.gameObject, 2f);


        int a = Random.Range(0, 4);
        a = 0; //Test
        Game2Manager.Instance.StopRingsRotating();
        switch(a)
        {
            case 0:
                //_spriteRender.gameObject.transform.DOMove(this.gameObject.transform.position, 1.5f).SetEase(Ease.OutElastic);
                _spriteRender.gameObject.transform.DOMove(this.gameObject.transform.position, 1.5f).SetEase(Ease.InExpo);
                break;

            case 1:
                _spriteRender.gameObject.transform.DOMove(this.gameObject.transform.position, 1.5f).SetEase(Ease.InOutBounce);
                break;

            case 2:
                _spriteRender.gameObject.transform.DOMove(this.gameObject.transform.position, 1.5f).SetEase(Ease.InOutCubic);
                break;

            case 3:
                _spriteRender.gameObject.transform.DOMove(this.gameObject.transform.position, 1.5f).SetEase(Ease.InQuint);
                break;

            case 4:
                _spriteRender.gameObject.transform.DOMove(this.gameObject.transform.position, 1.5f).SetEase(Ease.OutCirc);
                break;
        }


        //SFXManager.Instance.PlaySound("ta daah");

        yield return new WaitForSeconds(1.5f);
        _spriteRender.gameObject.transform.position = this.transform.position;
        Game2Manager.Instance.StartRingsRotating();
        //SFXManager.Instance.PlaySound("brah");
        FullRotateCoin();


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
        else if (_unit._Ability != null)
        {
            _Ability.gameObject.SetActive(true);
            _Ability.sprite = _unit._Ability._sprite;
        }
        else
        {
            _Ability.gameObject.SetActive(false);
        }

        //Done in the animation
        //Game2Manager.Instance.UpdateGameState(GameState.GamePlay);

        if(_unit._Ability != null)
            if(_unit._Ability.OnPull)
            {
                _unit._Ability.OnPullAbility();
            }

        //check the number of units and check if the danger level is too high after that
        if (Game2Manager.Instance.CheckDanger())
        {
            Game2Manager.Instance.DangerTooHigh();
        }
        else if (Game2Manager.Instance._ContainerManager._NumberOfUnitsPulled == Game2Manager.Instance._ActiveContainers)
        {
            yield return new WaitForSeconds(2);
            Game2Manager.Instance.UpdateGameState(GameState.CheckConditions);
        }

        yield return null;
    }

    public void DisableAllVisuals()
    {
        _Danger.SetActive(false);
        _Star.SetActive(false);
        _yellowresourceGO.SetActive(false);
        _GreenresourceGO.SetActive(false);
        _spriteRender.sprite = null;
        _Ability.sprite = null;
        _Coin.SetActive(false);
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

    public void FullRotateCoin()
    {
        StartCoroutine(RotateCoin());
    }

    IEnumerator RotateCoin()
    {
        if (_Coin == null) yield break;

        float rotationSpeed = 360f; // degrees per second
        float rotatedAmount = 0f;

        while (rotatedAmount < 360f)
        {
            float step = rotationSpeed * Time.deltaTime;

            _Coin.transform.Rotate(0f, 0f, step);

            rotatedAmount += step;

            yield return null;
        }

        // Snap exactly to full rotation (prevents tiny float errors)
        Vector3 rot = _Coin.transform.eulerAngles;
        rot.z = Mathf.Round(rot.z / 360f) * 360f;
        _Coin.transform.eulerAngles = rot;
    }


}
