using UnityEngine;
using YeLazzers.Buildings;

public class Game : MonoBehaviour
{
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private Station _stationPrefab;

    public void Initialize()
    {
        var station = Instantiate(_stationPrefab);
        station.Initialize(_resourceSpawner, Vector3.zero);
    }
}