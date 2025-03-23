using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextLogger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private Player Player;

    private void Update()
    {
        _textField.text =
        $"Speed: {Player._movementSystem.Speed}" +
        $"Health: {Player._healthSystem.Health}" +
        $"Buffs count: {Player._buffHandler.activeBuffs.Count}";
    }
}
