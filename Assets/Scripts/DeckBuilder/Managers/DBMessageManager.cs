using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DBMessageManager : Singleton<DBMessageManager>
{

    public TextMeshProUGUI _Text;
    public GameObject _Background;

    private void Start()
    {
        ClearMessage();
    }

    public void UpdateMessage(string _string)
    {
        _Background.SetActive(true);
        _Text.text = _string;
    }

    public void ClearMessage()
    {
        _Text.text = "";
        _Background.SetActive(false);
    }
}
