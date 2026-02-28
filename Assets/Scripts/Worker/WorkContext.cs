using UnityEngine;

public class WorkContext
{
    public ICollectable Resource { get; set; }

    public MainBuilding Building { get; set; }

    public Vector3 ManualTarget { get; set; }
}
