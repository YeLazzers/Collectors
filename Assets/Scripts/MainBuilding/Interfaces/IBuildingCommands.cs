using UnityEngine;

public interface IBuildingCommands
{
    void BuildUnit();
    void StartPlacingFlag();
    void BuildNewBuilding(BuildingConfig config, Vector3 position);
}
