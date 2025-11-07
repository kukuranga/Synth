using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DBMessageScreen : MonoBehaviour
{

    public TextMeshProUGUI _TextToUpdate;


    private void Start()
    {
        Game2Manager.Instance._DBMessageScreen = this;
    }

    public void UpdateMessage(string _txt)
    {
        _TextToUpdate.text = _txt;
    }
}
