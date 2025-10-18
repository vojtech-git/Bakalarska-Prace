using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public Transform player;
    public Vector3 offset = new Vector3(0, 10, -10);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (!player) return;

        // Vector3 desiredPosition = player.position + offset;
        transform.position = player.position + offset; // Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.LookAt(player.position);
    }
}
