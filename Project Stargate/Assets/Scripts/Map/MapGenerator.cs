// Assets/Scripts/Map/MapGenerator.cs
using UnityEngine;

public class MapGenerator
{
    public SegmentLibrary library;
    public MapDefinition output;
    public int width = 10, height = 10;
    public int seed = 0;
    public SegmentCategory startCategory = SegmentCategory.Start;

    [ContextMenu("Generate Logical Map")]
    public void Generate()
    {
        if (output == null || library == null) { Debug.LogError("Chybí library nebo output."); return; }
        if (seed != 0) Random.InitState(seed);

        output.Allocate(width, height);
        int cx = width / 2, cy = height / 2;

        for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
        {
            bool isCenter = x == cx && y == cy;
            var cat = isCenter ? startCategory : PickCategory();
            var def = library.GetRandom(cat);

            var c = output.Get(x, y);
            c.category = cat;
            c.scene = def;
            c.openN = y < height - 1;
            c.openE = x < width - 1;
            c.openS = y > 0;
            c.openW = x > 0;
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(output);
#endif
        Debug.Log("Mapa vygenerována logicky, bez instancování scén.");
    }

    private SegmentCategory PickCategory()
    {
        int r = Random.Range(0, 100);
        if (r < 20) return SegmentCategory.Safe;
        if (r < 50) return SegmentCategory.Enemy;
        if (r < 75) return SegmentCategory.Puzzle;
        return SegmentCategory.Hazard;
    }
}