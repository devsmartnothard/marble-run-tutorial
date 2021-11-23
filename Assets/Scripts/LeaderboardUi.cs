using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUi : MonoBehaviour
{
    [SerializeField]
    private CheckpointSystem _checkpointSystem;

    [SerializeField]
    private LeaderboardRow _rowPrefab;

    [SerializeField]
    private Transform _rowsContainer;
    
    private List<LeaderboardRow> _rows = new List<LeaderboardRow>();

    private void Awake()
    {
        _checkpointSystem.OnLeaderboardChanged += UpdateLeaderboard;
    }

    public void Initialize(IEnumerable<ICheckpointUser> racers)
    {
        var counter = 0;
        _rows.Clear();
        
        foreach (var racer in racers)
        {
            var row = LeanPool.Spawn(_rowPrefab, _rowsContainer);
            counter++;
            row.Initialize(counter, racer);
            _rows.Add(row);
        }
    }

    private void UpdateLeaderboard(IEnumerable<ICheckpointUser> sortedRacers)
    {
        var counter = 0;
        
        foreach (var racer in sortedRacers)
        {
            _rows[counter++].UpdatePlayer(racer);
        }
    }

    private void OnDestroy()
    {
        _checkpointSystem.OnLeaderboardChanged -= UpdateLeaderboard;
    }
}
