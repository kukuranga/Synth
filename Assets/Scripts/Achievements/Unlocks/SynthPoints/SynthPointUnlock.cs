using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Synth Point Unlock", menuName = "Unlocks/SynthPointUnlock", order = 51)]
public class SynthPointUnlock : Unlock
{

    public override void Bonus()
    {
        SynthManager.Instance.AddTotalUpgradePoint();
    }
}
