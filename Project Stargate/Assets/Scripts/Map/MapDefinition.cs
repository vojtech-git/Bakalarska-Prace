using UnityEngine;

[CreateAssetMenu(fileName = "MapDefinition", menuName = "Map/Map Definition")]
public class MapDefinition : ScriptableObject
{
    public int width = 10;
    public int height = 10;
    public Cell[] cells;

    public void Allocate(int w, int h)
    {
        width = w; height = h;
        cells = new Cell[w * h];
        for (int i = 0; i < cells.Length; i++) cells[i] = new Cell();
    }

    public Cell Get(int x, int y) => cells[y * width + x];
    
    public bool InBounds(int x, int y) => x >= 0 && y >= 0 && x < width && y < height;
}