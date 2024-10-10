using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Explanation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public NodeAchievement _NodeAch;
    public GameObject _ExplanationGO;
    public TextMeshProUGUI _Text;

    private bool isButtonHeldDown = false;

    // Called when the button is pressed down
    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonHeldDown = true;
        Debug.Log("Button is being held down");
        // You can perform any action here when the button is pressed down
    }

    // Called when the button is released
    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonHeldDown = false;
        Debug.Log("Button is released");
        // You can perform any action here when the button is released
    }

    // Update is called once per frame
    private void Update()
    {
        // You can track if the button is held down and continuously perform some action
        if (isButtonHeldDown)
        {
            _ExplanationGO.SetActive(true);
        }
        else
        {
            _ExplanationGO.SetActive(false);
        }
    }

    private void Start()
    {
        _Text.text = _NodeAch._Achievement._Unlock._Description;
    }
}
