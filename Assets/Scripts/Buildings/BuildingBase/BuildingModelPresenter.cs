using UnityEngine;

namespace YeLazzers.Buildings
{
    public class BuildingModelPresenter : MonoBehaviour
    {
        [SerializeField] private MeshViewArray _meshViewArray;

        public IMeshView MeshView => _meshViewArray;
    }
}
