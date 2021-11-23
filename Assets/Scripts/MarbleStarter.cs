using UnityEngine;

public class MarbleStarter : MonoBehaviour
{
    public float force = 10f;
    public ForceMode mode = ForceMode.Force;

    private void Start()
    {
        var rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, mode);
    }
}
