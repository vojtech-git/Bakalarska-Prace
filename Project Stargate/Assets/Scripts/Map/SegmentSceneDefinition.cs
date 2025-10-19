// Assets/Scripts/Map/SegmentSceneDefinition.cs
using UnityEngine;

[CreateAssetMenu(fileName = "SegmentScene", menuName = "Map/Segment Scene")]
public class SegmentSceneDefinition : ScriptableObject
{
    public string sceneName; // musí být v Build Settings
    public SegmentCategory category;

#if UNITY_EDITOR
    public UnityEditor.SceneAsset sceneAsset;
    private void OnValidate()
    {
        if (sceneAsset != null)
            sceneName = sceneAsset.name;
    }
#endif
}