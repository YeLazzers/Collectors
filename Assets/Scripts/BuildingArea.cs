using UnityEngine;

public class BuildingArea : MonoBehaviour
{
    [SerializeField] private Color _areaColor = new Color(0, 1, 0, 0.3f);
    [SerializeField] private float _modelScale = 0.1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = _areaColor;
        Gizmos.DrawCube(transform.position, new Vector3(transform.localScale.x * _modelScale, 0.1f, transform.localScale.z * _modelScale));
    }
}
