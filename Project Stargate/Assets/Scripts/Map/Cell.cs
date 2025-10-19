// Assets/Scripts/Map/MapDefinition.cs
using UnityEngine;

[System.Serializable]
public class Cell
{
    public SegmentCategory category;
    public SegmentSceneDefinition scene;
    public bool openN, openE, openS, openW;
}