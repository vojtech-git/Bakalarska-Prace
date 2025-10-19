// Assets/Scripts/Map/SegmentLibrary.cs
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SegmentSceneLibrary", menuName = "Map/Segment Scene Library")]
public class SegmentLibrary : ScriptableObject
{
    [System.Serializable]
    public class Bucket
    {
        public SegmentCategory category;
        public List<SegmentSceneDefinition> scenes = new();
    }

    public List<Bucket> buckets = new();

    public SegmentSceneDefinition GetRandom(SegmentCategory cat)
    {
        var b = buckets.Find(x => x.category == cat);
        if (b == null || b.scenes.Count == 0) return null;
        return b.scenes[Random.Range(0, b.scenes.Count)];
    }
}