// Assets/Scripts/Map/MapRuntime.cs
using UnityEngine;
using UnityEngine.SceneManagement;

// Vytrvalý runtime stav. Přenos mezi scénami přes DontDestroyOnLoad.
public class MapRuntime : MonoBehaviour
{
    public static MapRuntime I { get; private set; }

    public int width;
    public int height;
    public SegmentLibrary library;
    public MapDefinition output;
    public Vector2Int current;         // aktuální souřadnice
    public Dir lastEnterDir = Dir.South; // směr, odkud hráč vstoupil do aktuální scény

    private void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartAtCenter()
    {
        MapGenerator generator = new MapGenerator();
        generator.library = library;
        generator.output = output;
        generator.Generate();
        
        current = new Vector2Int(output.width / 2, output.height / 2);
        lastEnterDir = Dir.South;
        LoadCurrentScene();
    }

    public bool CanMove(Dir d)
    {
        var c = output.Get(current.x, current.y);
        return d switch
        {
            Dir.North => c.openN && output.InBounds(current.x, current.y + 1),
            Dir.East  => c.openE && output.InBounds(current.x + 1, current.y),
            Dir.South => c.openS && output.InBounds(current.x, current.y - 1),
            Dir.West  => c.openW && output.InBounds(current.x - 1, current.y),
            _ => false
        };
    }

    public async void Move(Dir d)
    {
        if (!CanMove(d)) return;
        switch (d)
        {
            case Dir.North: current.y += 1; lastEnterDir = Dir.South; break;
            case Dir.East:  current.x += 1; lastEnterDir = Dir.West;  break;
            case Dir.South: current.y -= 1; lastEnterDir = Dir.North; break;
            case Dir.West:  current.x -= 1; lastEnterDir = Dir.East;  break;
        }
        LoadCurrentScene();
    }

    private void LoadCurrentScene()
    {
        var cell = output.Get(current.x, current.y);
        var sceneName = cell.scene != null ? cell.scene.sceneName : null;
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError($"V buňce [{current.x},{current.y}] není přiřazena scéna.");
            return;
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        // po načtení scény SceneEntryPoint přečte MapRuntime.I.lastEnterDir a umístí hráče
    }
}
    