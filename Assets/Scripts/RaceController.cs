using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

public class RaceController : MonoBehaviour
{
    [SerializeField]
    private PlayerCreator _playerCreator;

    [SerializeField]
    private LeaderboardUi _leaderboard;

    [SerializeField]
    private CheckpointSystem _checkpointSystem;

    [SerializeField]
    private Marble _playerPrefab;

    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField]
    private Button _startRaceButton;

    [SerializeField]
    private SpeedBoost _startingSpeedBoost;

    [SerializeField]
    private List<GameObject> _blockades = new List<GameObject>();
    
    private List<ICheckpointUser> _racers = new List<ICheckpointUser>();

    private void Awake()
    {
        _playerCreator.OnSpawnRequested += SpawnPlayer;
        _startRaceButton.onClick.AddListener(StartRace);
        _startingSpeedBoost.enabled = false;
        _blockades.ForEach(b => b.SetActive(true));
    }

    private void SpawnPlayer(string playerName, Color playerColor)
    {
        var marble = LeanPool.Spawn(_playerPrefab, _spawnPoint.position, Quaternion.identity);
        marble.Name = playerName;
        marble.Color = playerColor;
        marble.ChangeColor(playerColor);
        _racers.Add(marble);
    }

    private void StartRace()
    {
        _leaderboard.Initialize(_racers);
        _checkpointSystem.InitializeRacers(_racers);
        _blockades.ForEach(b => b.SetActive(false));
        _startingSpeedBoost.enabled = true;
        StartCoroutine(DisableStartingBoost());
        _playerCreator.gameObject.SetActive(false);
    }

    private IEnumerator DisableStartingBoost()
    {
        yield return new WaitForSeconds(5f);
        _startingSpeedBoost.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _playerCreator.OnSpawnRequested -= SpawnPlayer;
        _startRaceButton.onClick.RemoveAllListeners();
    }
}