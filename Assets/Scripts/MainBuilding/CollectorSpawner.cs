using UnityEngine;

public class CollectorSpawner : MonoBehaviour
{
    [SerializeField] private int _initialCollectorsCount = 3;
    [SerializeField] private Collector _prefab;
    [SerializeField] private float _spawnRadius = 1f;

    private float _yPosition = 1f;
    private float _randomRotationOffset;

    private void Start()
    {
        _randomRotationOffset = Random.Range(0f, Mathf.PI * 2f);

        for (int i = 0; i < _initialCollectorsCount; i++)
        {
            Instantiate(_prefab, GetSpawnPosition(i, _initialCollectorsCount), Quaternion.identity);
        }
    }

    private Vector3 GetSpawnPosition(int index, int total)
    {
        float range = 2 * Mathf.PI / total;
        float angle = index * range + _randomRotationOffset;
        float x = _spawnRadius * Mathf.Cos(angle);
        float z = _spawnRadius * Mathf.Sin(angle);

        return new Vector3(
            transform.position.x + x,
            _yPosition,
            transform.position.z + z
        );
    }
}