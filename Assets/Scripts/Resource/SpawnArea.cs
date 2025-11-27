using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    private readonly float _radiusMultiplier = 2f;
    private readonly float _zScale = 1f;

    [SerializeField] private Vector3 _centerOffset;
    [SerializeField] private float _width = 1f;
    [SerializeField] private float _spawnRingInner = 2f;
    [SerializeField] private float _spawnRingOuter = 5f;

    private void OnValidate()
    {
        transform.localScale = new Vector3(_spawnRingOuter * _radiusMultiplier, _spawnRingOuter * _radiusMultiplier, _zScale);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(Color.green.r, Color.green.g, Color.green.b, 0.3f);
        Gizmos.DrawCube(transform.position + _centerOffset, new Vector3(_width, _centerOffset.y, _width));
    }

    public Vector3 GetRandomPointInArea(Vector3 center)
    {
        return new Vector3(
            Random.Range(-_width / 2f, _width / 2f),
            0,
            Random.Range(-_width / 2f, _width / 2f)
        ) + center + _centerOffset;
    }
}