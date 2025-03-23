using System.Collections.Generic;
using UnityEngine;

public abstract class Buff<T> : ScriptableObject, IBuff where T : BuffableComponent
{
    public T Component { get; set; }
    public bool IsExpired { get; set; }
    public virtual bool Stackable { get; }
    public List<IBuff> Stacks { get; set; } = new();

    public virtual void OnApplied() { }
    public virtual void OnExpired() { }
    public virtual void OnUpdate() { }

    public void AddStack(IBuff buff)
    {
        Stacks.Add(buff);
    }
}

public interface IBuff
{
    void OnApplied();
    void OnExpired();
    void OnUpdate();
    bool IsExpired { get; set; }

}