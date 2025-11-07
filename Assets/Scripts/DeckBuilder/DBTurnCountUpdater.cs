using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DBTurnCountUpdater : MonoBehaviour
{

    public TextMeshProUGUI _TurnCount;

    private void Update()
    {
        _TurnCount.text = Game2Manager.Instance._CurrentTurnCount.ToString();
    }

}
