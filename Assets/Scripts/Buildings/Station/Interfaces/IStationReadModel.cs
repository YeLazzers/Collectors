using System;

namespace YeLazzers.Buildings
{
    public interface IStationReadModel
    {
        string BuildingName { get; }
        int ResourcesCount { get; }
        int UnitsCount { get; }
        bool CanBuildUnit { get; }
        bool CanPlaceFlag { get; }
        bool CanBuildNewBase { get; }

        event Action<IStationReadModel> BuildingUpdated;
    }
}
