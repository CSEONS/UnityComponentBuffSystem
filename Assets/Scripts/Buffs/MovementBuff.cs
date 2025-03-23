using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingBuff", menuName = "Buffs/HealingBuff")]

public class MovementBuff : Buff<MovementSystem>
{
    public override void OnApplied()
    {
        Component.Speed += 10;
    }

    public override void OnExpired()
    {
        Component.Speed -= 10;
    }
}
