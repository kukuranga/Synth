using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpOnActive : MonoBehaviour
{
    [SerializeField] bool _OnActive = false;
    [SerializeField] bool _Loadingscreen = false;
    [SerializeField] bool _Loop = false;
    private Vector2 _StartingPos;
    private RectTransform _RT;
    public RectTransform _OutsidePosition;
    public float _Speed;
    


    private void Awake()
    {
        _RT = this.GetComponent<RectTransform>();
        _StartingPos = _RT.anchoredPosition;
    }

    private void Start()
    {
        if (!_OnActive)
        {
            if (_Loadingscreen)
            {
                _RT.anchoredPosition = _OutsidePosition.anchoredPosition;
                _Loadingscreen = false;
            }
            else
                SceneLoader.Instance.onSceneLoadedEvent.AddListener(MoveDown);
        }
    }

    private void OnEnable()
    {
        if (_OnActive)
        {
            if (_Loadingscreen)
            {
                _RT.anchoredPosition = _OutsidePosition.anchoredPosition;
                _Loadingscreen = false;
            }
            else
                SceneLoader.Instance.onSceneLoadedEvent.AddListener(MoveDown);
        }
    }

    private void FixedUpdate()
    {
        LerpToPoint();
    }

    private void MoveDown(List<string> s)
    {
        _RT.anchoredPosition = _OutsidePosition.anchoredPosition;
    }

    private void LerpToPoint()
    {
        float step = _Speed * Time.deltaTime;
        _RT.anchoredPosition = Vector2.Lerp(_RT.anchoredPosition, _StartingPos, step);
        if(_Loop)
        {
            if(Vector2.Distance(_RT.anchoredPosition, _StartingPos) < 400)
            {
                _RT.anchoredPosition = _OutsidePosition.anchoredPosition;
            }
        }
    }


}
