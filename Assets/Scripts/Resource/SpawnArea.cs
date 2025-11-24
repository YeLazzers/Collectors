using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    private readonly float _radiusMultiplier = 2f;
    private readonly float _zScale = 1f;

    [SerializeField] private Vector3 _centerOffset;
    [SerializeField] private float _spawnRingInner = 2f;
    [SerializeField] private float _spawnRingOuter = 5f;

    private void OnValidate()
    {
        transform.localScale = new Vector3(_spawnRingOuter * _radiusMultiplier, _spawnRingOuter * _radiusMultiplier, _zScale);
    }

    public Vector3 RandomPointInSquareRing(Vector3 center)
    {
        while (true)
        {
            float x = Random.Range(-_spawnRingOuter, _spawnRingOuter);
            float z = Random.Range(-_spawnRingOuter, _spawnRingOuter);

            if (Mathf.Max(Mathf.Abs(x), Mathf.Abs(z)) >= _spawnRingInner)
                return new Vector3(x, 0, z) + center + _centerOffset;
        }
    }

    public Vector3 RandomPointInCircleRing(Vector3 center)
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float radius = Random.Range(_spawnRingInner, _spawnRingOuter);
        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);

        return new Vector3(x, 0, z) + center + _centerOffset;
    }
}