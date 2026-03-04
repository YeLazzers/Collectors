using UnityEngine;
using YeLazzers.Buildings;

public class Game : MonoBehaviour
{
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private Station _buildingPrefab;
    [SerializeField] private BuildingConfig _factoryConfig;

    public void Initialize()
    {
        var building = Instantiate(_buildingPrefab);
        building.Initialize(_resourceSpawner, _factoryConfig, Vector3.zero);
    }

    
}