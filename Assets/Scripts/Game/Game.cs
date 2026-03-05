using UnityEngine;
using YeLazzers.Buildings;

public class Game : MonoBehaviour
{
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private Station _stationPrefab;
    [SerializeField] private BuildingConfig _stationConfig;

    public void Initialize()
    {
        var station = Instantiate(_stationPrefab);
        station.Initialize(_resourceSpawner, _stationConfig, Vector3.zero);
    }
}