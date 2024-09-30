using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Goals UI Unlock", menuName = "Unlocks/UIGoalsUnlock", order = 51)]
public class GoalsUnlock : Unlock
{

    public override void Bonus()
    {
        GameManager.Instance.GoalsUnlocked = true;
    }
}
