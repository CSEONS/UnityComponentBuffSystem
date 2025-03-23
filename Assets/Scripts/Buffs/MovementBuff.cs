using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementBuff", menuName = "Buffs/MovementBuff")]

public class MovementBuff : Buff<MovementSystem>
{
    //TODO: Вынести все свойства по типу Stackable Duration и прочего в сам бафф и  регулировать каждый самостоятельно
    public override bool Stackable => _stackable;

    public float SpeedBuff;
    public float Duration;

    [SerializeField]
    private bool _stackable;

    private float _expiredTime;

    public override void OnApplied()
    {
        if (Stackable)
        {
            Stacks.ForEach(s =>
            {
                var stack = s as MovementBuff;
                Component.Speed += stack.SpeedBuff;
            });
        }
        else
        {
            Component.Speed += SpeedBuff;
        }
    }

    public override void OnUpdate()
    {
        _expiredTime += Time.deltaTime;

        if (_expiredTime >= Duration)
        {
            IsExpired = true;
        }
    }

    public override void OnExpired()
    {
        if (Stackable)
        {
            Stacks.ForEach(s =>
            {
                var stack = s as MovementBuff;
                Component.Speed -= stack.SpeedBuff;
            });
        }
        else
        {
            Component.Speed -= SpeedBuff;
        }
    }
}
