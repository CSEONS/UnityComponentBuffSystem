using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    [SerializeField]
    private List<BuffableComponent> buffableComponents = new();

    private List<IBuff> activeBuffs = new(); // Теперь используем интерфейс IBuff

    public void Apply<T>(Buff<T> buff) where T : BuffableComponent
    {
        if (buff == null)
        {
            Debug.LogError("Buff is null!");
            return;
        }

        // Ищем компонент типа T в списке buffableComponents
        var component = buffableComponents.FirstOrDefault(c => c is T) as T;

        if (component == null)
        {
            Debug.LogError($"Component of type {typeof(T)} not found in buffableComponents!");
            return;
        }

        // Устанавливаем компонент для баффа
        buff.Component = component;

        // Обрабатываем стекующиеся баффы
        HanldeStackableBuffs(buff, component);

        // Вызываем метод OnApplied баффа
        buff.OnApplied();
    }

    public void Update()
    {
        foreach (var buff in activeBuffs)
        {
            buff.OnUpdate();

            if (buff.IsExpired)
            {
                buff.OnExpired();
            }
        }

        // Удаляем истекшие баффы
        activeBuffs.RemoveAll(buff => buff.IsExpired);
    }

    private void HanldeStackableBuffs<T>(Buff<T> buff, T component) where T : BuffableComponent
    {
        if (component == null)
        {
            Debug.LogError("Component is null!");
            return;
        }

        // Если бафф стекующийся, добавляем его в список
        if (buff.Stackable)
        {
            activeBuffs.Add(buff); // Теперь buff реализует IBuff, поэтому его можно добавить
        }
        // Если бафф не стекующийся и уже есть в списке, не добавляем его
        else if (activeBuffs.Any(b => b.GetType() == buff.GetType()))
        {
            Debug.Log($"Buff of type {buff.GetType()} already exists and is not stackable.");
            return;
        }
        // Если бафф не стекующийся и его нет в списке, добавляем его
        else
        {
            activeBuffs.Add(buff); // Теперь buff реализует IBuff, поэтому его можно добавить
        }
    }
}