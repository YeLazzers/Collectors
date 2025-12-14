using System;

public interface IBuildingReadModel
{
    string BuildingName { get; }
    int ResourcesCount { get; }
    int UnitsCount { get; }
    bool CanBuildUnit { get; }
    bool CanPlaceFlag { get; }
    bool CanBuildNewBase { get; }

    event Action<IBuildingReadModel> BuildingUpdated;
}
