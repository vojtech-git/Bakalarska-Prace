using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Gate : MonoBehaviour
{
    public Dir direction;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        if (MapRuntime.I != null) MapRuntime.I.Move(direction);
    }
}