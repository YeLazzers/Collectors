using UnityEngine;

namespace YeLazzers.Buildings
{
    [CreateAssetMenu(fileName = "BuildingConfig", menuName = "Buildings/New Building", order = 52)]
    public class BuildingConfig : ScriptableObject
    {
        public Building Prefab;
        public string Name;
        public int Cost;
        public MeshView Model;
        public Vector2 FootprintSize;
    }
}
