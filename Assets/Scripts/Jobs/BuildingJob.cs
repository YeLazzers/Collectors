using UnityEngine;

public class BuildingJob : JobBase
{
    public BuildingConfig Config { get; private set; }
    public Vector3 Position { get; private set; }

    public BuildingJob(MainBuilding source, BuildingConfig config, Vector3 position) : base(source)
    {
        Config = config;
        Position = position;
    }
}