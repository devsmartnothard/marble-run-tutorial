using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreator : MonoBehaviour
{
    public event Action<string, Color> OnSpawnRequested;
    
    [SerializeField]
    private List<Color> _availableColors = new List<Color>();
    
    [SerializeField]
    private Button _changeColorLeft;

    [SerializeField]
    private Button _changeColorRight;

    [SerializeField]
    private TMP_InputField _inputField;

    [SerializeField]
    private TextMeshProUGUI _nameLabel;

    [SerializeField]
    private Button _spawnButton;

    private int _chosenColorIndex;

    public Color ChosenColor => _availableColors[_chosenColorIndex];

    private void Awake()
    {
        UpdateNameColor();
        _changeColorLeft.onClick.AddListener(SwitchColorLeft);
        _changeColorRight.onClick.AddListener(SwitchColorRight);
        _inputField.onValueChanged.AddListener(UpdatePlayerName);
        _spawnButton.onClick.AddListener(RequestSpawn);
    }

    private void UpdateNameColor()
    {
        _nameLabel.color = ChosenColor;
    }
    
    private void SwitchColorLeft()
    {
        _chosenColorIndex = (int)Mathf.Repeat(_chosenColorIndex - 1, _availableColors.Count);
        UpdateNameColor();
    }

    private void SwitchColorRight()
    {
        _chosenColorIndex = (int)Mathf.Repeat(_chosenColorIndex + 1, _availableColors.Count);
        UpdateNameColor();
    }

    private void UpdatePlayerName(string input)
    {
        _nameLabel.SetText(input);
    }

    private void RequestSpawn()
    {
        OnSpawnRequested?.Invoke(_nameLabel.text, ChosenColor);
    }

    private void OnDestroy()
    {
        _changeColorLeft.onClick.RemoveAllListeners();
        _changeColorRight.onClick.RemoveAllListeners();
        _inputField.onValueChanged.RemoveAllListeners();
        _spawnButton.onClick.RemoveAllListeners();
    }
}