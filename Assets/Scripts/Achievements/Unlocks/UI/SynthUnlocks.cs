using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Synth UI Unlock", menuName = "Unlocks/UISynthUnlock", order = 51)]
public class SynthUnlocks : Unlock
{

    public override void Bonus()
    {
        GameManager.Instance.SynthUnlocked = true;
    }
}
