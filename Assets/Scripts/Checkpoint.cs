using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public event Action<ICheckpointUser, Checkpoint> OnCheckpointEntered;

    private void OnTriggerEnter(Collider other)
    {
        var racer = other.GetComponent<ICheckpointUser>();

        if (racer != null)
        {
            OnCheckpointEntered?.Invoke(racer, this);
        }
    }
}
