using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    [SerializeField]
    private List<BuffableComponent> buffableComponents = new();

    public List<IBuff> activeBuffs = new(); // ������ ���������� ��������� IBuff

    public void Apply<T>(Buff<T> buff) where T : BuffableComponent
    {
        buff = Instantiate(buff);

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
        if (buff.Stackable)
        {
            activeBuffs.Add(buff); // ������ buff ��������� IBuff, ������� ��� ����� ��������
            buff.AddStack(buff);
            buff.OnApplied(); // �������� OnApplied ������ ���� ���� ��� ��������
        }
        // ���� ���� �� ����������� � ��� ���� � ������, �� ��������� ���
        else if (activeBuffs.Any(b => b.GetType() == buff.GetType()))
        {
            buff.AddStack(buff);
            Debug.Log($"Buff of type {buff.GetType()} already exists and is not stackable.");
        }
        // ���� ���� �� ����������� � ��� ��� � ������, ��������� ���
        else
        {
            activeBuffs.Add(buff); // ������ buff ��������� IBuff, ������� ��� ����� ��������
            buff.OnApplied(); // �������� OnApplied ������ ���� ���� ��� ��������
        }
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
}