using UnityEngine;

public abstract class Buff<T> : ScriptableObject, IBuff where T : BuffableComponent
{
    public T Component { get; set; }
    public bool IsExpired { get; set; }
    public virtual bool Stackable { get; }

    public virtual void OnApplied() { }
    public virtual void OnExpired() { }
    public virtual void OnUpdate() { }
}

public interface IBuff
{
    void OnApplied();
    void OnExpired();
    void OnUpdate();
    bool IsExpired { get; set; }

}