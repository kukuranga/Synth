using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeScreenTutorial : MonoBehaviour
{
    public GameObject _StartButtonTut;
    public TextMeshProUGUI _Text;
    public List<TutorialSO> _Tutorials;
    private Canvas _canvas;
    private Vector2 _OriginalPosition;
    private int _Index = 0;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();

        if (TutorialManager.Instance._StartButtonTutorial)
        {
            _StartButtonTut.SetActive(true);
            TutorialManager.Instance._StartButtonTutorial = false;
        }

        _OriginalPosition = _StartButtonTut.transform.position;

        if(TutorialManager.Instance._StartButtonTutorial)
        {
            _StartButtonTut.SetActive(true);
        }
    }

    public void Next()
    {
        TutorialSO tut = _Tutorials[_Index++];
        if(tut._EndingTut)
        {
            _StartButtonTut.SetActive(false);
            TutorialManager.Instance._StartButtonTutorial = false;
        }
        else
        {
            _Text.text = tut._Text;
            
            if(tut._Move)
                _StartButtonTut.transform.position = tut._Position;
        }

    }

    public void MoveToFront()
    {
        _canvas.sortingOrder = 10;
    }

    public void MoveToBack()
    {
        _canvas.sortingOrder = -1;
    }
}
