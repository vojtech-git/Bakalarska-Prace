// Assets/Scripts/Map/SceneEntryPoint.cs
using UnityEngine;

// Umísti jeden objekt tohoto typu do KAŽDÉ cílové scény a nastav spawn body pro příchozí směr.
public class SceneEntryPoint : MonoBehaviour
{
    public Transform fromNorth;
    public Transform fromEast;
    public Transform fromSouth;
    public Transform fromWest;
    public Transform playerPrefabIfNeeded; // volitelné: auto-spawn hráče, pokud není persistentní

    private void Start()
    {
        var dir = MapRuntime.I != null ? MapRuntime.I.lastEnterDir : Dir.South;
        Transform t = dir switch
        {
            Dir.North => fromNorth,
            Dir.East  => fromEast,
            Dir.South => fromSouth,
            Dir.West  => fromWest,
            _ => fromSouth
        };

        var player = FindObjectOfType<PlayerController>(); // tvůj hráč skript
        if (player == null && playerPrefabIfNeeded != null)
            player = Instantiate(playerPrefabIfNeeded, Vector3.zero, Quaternion.identity).GetComponent<PlayerController>();

        if (player != null && t != null)
        {
            player.transform.position = t.position;
            player.transform.rotation = t.rotation;
        }
    }
}