
using UnityEngine;

public interface IBuildingCommands
{
    void BuildUnit();
    void StartPlacingFlag();
    void BuildNewBaseAt(Vector3 position);
}
