using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public event Action<IEnumerable<ICheckpointUser>> OnLeaderboardChanged;

    [SerializeField]
    private List<Checkpoint> _checkpoints = new List<Checkpoint>();

    private Dictionary<ICheckpointUser, CheckpointScore> _checkpointScores = new Dictionary<ICheckpointUser, CheckpointScore>();
    private Dictionary<ICheckpointUser, Checkpoint>
        _expectedCheckpoints = new Dictionary<ICheckpointUser, Checkpoint>();

    private void Start()
    {
        _checkpoints.ForEach(c => c.OnCheckpointEntered += HandleCheckpointEntered);
    }

    private void HandleCheckpointEntered(ICheckpointUser racer, Checkpoint checkpoint)
    {
        if (_expectedCheckpoints.TryGetValue(racer, out var expectedCheckpoint))
        {
            if (checkpoint == expectedCheckpoint)
            {
                var checkpointIndex = _checkpoints.IndexOf(expectedCheckpoint);
                var nextCheckpointIndex = (checkpointIndex + 1) % _checkpoints.Count;
                _expectedCheckpoints[racer] = _checkpoints[nextCheckpointIndex];

                if (_checkpointScores.TryGetValue(racer, out var score))
                {
                    _checkpointScores[racer] = new CheckpointScore(++score.numberOfCheckpointsReached, Time.time);

                    OnLeaderboardChanged?.Invoke(SortRacers());
                }
                else
                {
                    Debug.LogError("Expected racer to be in dictionary.");
                }
            }
        }
    }

    private IEnumerable<ICheckpointUser> SortRacers()
    {
        var scores = _checkpointScores.ToList();
        scores.Sort((entry2, entry1) => entry1.Value.CompareTo(entry2.Value));

        return scores.Select(s => s.Key);
    }

    public void InitializeRacers(IEnumerable<ICheckpointUser> racers)
    {
        foreach (var racer in racers)
        {
            _expectedCheckpoints.Add(racer, _checkpoints.First());
            _checkpointScores.Add(racer, new CheckpointScore(0, Time.time));
            
            OnLeaderboardChanged?.Invoke(SortRacers());
        }
    }

    private void OnDestroy()
    {
        _checkpoints.ForEach(c => c.OnCheckpointEntered -= HandleCheckpointEntered);
    }
}
