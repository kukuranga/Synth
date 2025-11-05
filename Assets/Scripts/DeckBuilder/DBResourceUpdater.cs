using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DBResourceUpdater : MonoBehaviour
{

    public TextMeshProUGUI _YellowText;
    public TextMeshProUGUI _GreenText;

    // Update is called once per frame
    void Update()
    {
        _YellowText.text = Game2Manager.Instance._YellowResource.ToString();
        _GreenText.text = Game2Manager.Instance._GreenResource.ToString();

    }
}
