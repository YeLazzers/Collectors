using System.Linq;
using UnityEngine;

namespace YeLazzers.Buildings
{
    public class BuildingFootprint : MonoBehaviour
    {
        [SerializeField] private float _height;

        public void Initialize(Vector2 size)
        {
            transform.localScale = new Vector3(size.x, _height, size.y);
            Hide();
        }

        public void Show()
            => gameObject.SetActive(true);

        public void Hide()
            => gameObject.SetActive(false);

        public bool HasOverlapWithFootprint(LayerMask mask, GameObject[] ignoreObjects = null)
        {
            Collider[] colliders = Physics.OverlapBox(
                transform.position,
                transform.localScale / 2,
                Quaternion.identity,
                mask
            );

            Collider[] filtered = colliders
                .Where(col => col.gameObject != gameObject)
                .Where(col => ignoreObjects == null || ignoreObjects.FirstOrDefault(obj => obj == col.gameObject) == null)
                .ToArray();

            return filtered.Length > 0;
        }
    }
}