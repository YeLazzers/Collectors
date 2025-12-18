using UnityEngine;

public class BuildingModelPresenter : MonoBehaviour
{
    [SerializeField] private MeshViewArray _meshViewArray;

    public IMeshView MeshView => _meshViewArray;
}