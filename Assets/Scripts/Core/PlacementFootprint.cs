using System.Linq;
using UnityEngine;

public class PlacementFootprint : MonoBehaviour
{
    [SerializeField] private float _height;

    public void Initialize(Vector2 size)
    {
        transform.localScale = new Vector3(size.x, _height, size.y);
    }

    public bool HasOverlapWithMask(LayerMask mask)
    {

        Collider[] colliders = Physics.OverlapBox(
            transform.position,
            transform.localScale / 2,
            Quaternion.identity,
            mask
        );

        Collider[] filtered = colliders.Where(col => col.gameObject != gameObject).ToArray();

        return filtered.Length > 0;
    }
}