using UnityEngine;
using UnityEngine.UI;
using YeLazzers.Buildings;

public class BuildingSelectionView : MonoBehaviour
{
    // [SerializeField] private ValueRow _resourceRowPrefab;
    [SerializeField] private ValueRow _resources;
    [SerializeField] private ValueRow _workers;

    private void Awake()
    {

    }

    public void RenderStats(IStationReadModel model)
    {
        _resources.SetAmount(model.ResourcesCount);

        _workers.SetAmount(model.UnitsCount);
    }
}
