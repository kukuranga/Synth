using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DBResourceUpdater : MonoBehaviour
{

    public TextMeshProUGUI _YellowText;
    public TextMeshProUGUI _GreenText;
    public TextMeshProUGUI _DangerText;
    public TextMeshProUGUI _StarText;

    // Update is called once per frame
    void Update()
    {
        _YellowText.text = Game2Manager.Instance._YellowResource.ToString();
        _GreenText.text = Game2Manager.Instance._GreenResource.ToString();
        if (Game2Manager.Instance._gameState == GameState.GamePlay)
        {
            _DangerText.text = Game2Manager.Instance._ContainerManager.GetNumberOfDangerActive().ToString();
            _StarText.text = Game2Manager.Instance._ContainerManager.GetNumberOfStarsActive().ToString();
        }
    }
}
