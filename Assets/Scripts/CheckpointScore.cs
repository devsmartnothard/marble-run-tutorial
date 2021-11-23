using System;

public struct CheckpointScore : IComparable<CheckpointScore>
{
    public int numberOfCheckpointsReached;
    public float timeOnLastCheckpoint;

    public CheckpointScore(int numberOfCheckpointsReached, float timeOnLastCheckpoint)
    {
        this.numberOfCheckpointsReached = numberOfCheckpointsReached;
        this.timeOnLastCheckpoint = timeOnLastCheckpoint;
    }

    public int CompareTo(CheckpointScore other)
    {
        if (numberOfCheckpointsReached != other.numberOfCheckpointsReached)
        {
            // higher number of checkpoints reached wins
            return numberOfCheckpointsReached.CompareTo(other.numberOfCheckpointsReached);
        }
        else
        {
            // if checkpoints reached is same compare checkpoint reach times
            return other.timeOnLastCheckpoint.CompareTo(timeOnLastCheckpoint);
        }
    }
}