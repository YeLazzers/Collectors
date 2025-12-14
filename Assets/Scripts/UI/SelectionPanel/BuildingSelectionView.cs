using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectionView : MonoBehaviour
{
    // [SerializeField] private ValueRow _resourceRowPrefab;
    [SerializeField] private ValueRow _resources;
    [SerializeField] private ValueRow _workers;

    private void Awake()
    {

    }

    public void RenderStats(IBuildingReadModel model)
    {
        _resources.SetAmount(model.ResourcesCount);

        _workers.SetAmount(model.UnitsCount);
    }
}
