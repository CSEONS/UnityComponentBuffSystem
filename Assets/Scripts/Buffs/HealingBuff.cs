using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingBuff", menuName = "Buffs/HealingBuff")]
public class HealingBuff : Buff<HealthSystem> 
{
    public override bool Stackable => _stackable;

    public float HealOnTick;
    public float TickRate;
    public float HealValue;
    public float Duration;

    [SerializeField]
    private bool _stackable;

    private float _tickrateTime;
    private float _expiredTime;

    public override void OnUpdate()
    {
        _tickrateTime += Time.deltaTime;
        _expiredTime += Time.deltaTime;

        if (_expiredTime > Duration)
        {
            IsExpired = true;
        }

        if ((_tickrateTime / TickRate) >= 1)
        {
            OnTick();
            _tickrateTime = 0;
        }
    }

    public void OnTick()
    {
        Component.Health += HealValue;

        Debug.Log("Total HP: " + Component.Health);
    }
}