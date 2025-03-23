using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    [SerializeField]
    private List<BuffableComponent> buffableComponents = new();

    private List<IBuff> activeBuffs = new(); // ������ ���������� ��������� IBuff

    public void Apply<T>(Buff<T> buff) where T : BuffableComponent
    {
        if (buff == null)
        {
            Debug.LogError("Buff is null!");
            return;
        }

        // ���� ��������� ���� T � ������ buffableComponents
        var component = buffableComponents.FirstOrDefault(c => c is T) as T;

        if (component == null)
        {
            Debug.LogError($"Component of type {typeof(T)} not found in buffableComponents!");
            return;
        }

        // ������������� ��������� ��� �����
        buff.Component = component;

        // ������������ ����������� �����
        HanldeStackableBuffs(buff, component);

        // �������� ����� OnApplied �����
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

        // ������� �������� �����
        activeBuffs.RemoveAll(buff => buff.IsExpired);
    }

    private void HanldeStackableBuffs<T>(Buff<T> buff, T component) where T : BuffableComponent
    {
        if (component == null)
        {
            Debug.LogError("Component is null!");
            return;
        }

        // ���� ���� �����������, ��������� ��� � ������
        if (buff.Stackable)
        {
            activeBuffs.Add(buff); // ������ buff ��������� IBuff, ������� ��� ����� ��������
        }
        // ���� ���� �� ����������� � ��� ���� � ������, �� ��������� ���
        else if (activeBuffs.Any(b => b.GetType() == buff.GetType()))
        {
            Debug.Log($"Buff of type {buff.GetType()} already exists and is not stackable.");
            return;
        }
        // ���� ���� �� ����������� � ��� ��� � ������, ��������� ���
        else
        {
            activeBuffs.Add(buff); // ������ buff ��������� IBuff, ������� ��� ����� ��������
        }
    }
}