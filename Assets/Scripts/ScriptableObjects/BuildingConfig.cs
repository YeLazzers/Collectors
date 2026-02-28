using UnityEngine;

[CreateAssetMenu(fileName = "BuildingConfig", menuName = "Buildings/New Building", order = 52)]
public class BuildingConfig : ScriptableObject
{
    public string Name;
    public int Cost;
    public BuildingModelPresenter Model;
    public Vector2 footprintSize;
}