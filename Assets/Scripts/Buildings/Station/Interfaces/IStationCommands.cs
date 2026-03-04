using UnityEngine;

namespace YeLazzers.Buildings
{
    public interface IStationCommands
    {
        void BuildUnit();
        void StartPlacingFlag();
        void BuildNewBuilding(BuildingConfig config, Vector3 position);
    }
}
