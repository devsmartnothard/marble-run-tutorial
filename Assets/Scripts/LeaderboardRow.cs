using TMPro;
using UnityEngine;

public class LeaderboardRow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _number;

    [SerializeField]
    private TextMeshProUGUI _playerName;

    public void Initialize(int number, ICheckpointUser racer)
    {
        _number.SetText($"{number}.");
        _playerName.SetText(racer.Name);
        _playerName.color = racer.Color;
    }

    public void UpdatePlayer(ICheckpointUser racer)
    {
        _playerName.SetText(racer.Name);
        _playerName.color = racer.Color;
    }
}