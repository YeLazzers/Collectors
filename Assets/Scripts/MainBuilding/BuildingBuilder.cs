using UnityEngine;

public class BuildingBuilder : MonoBehaviour
{
    [SerializeField] private CollectorHub _hub;

    private BuildingPlacementPreview _preview;

    public void InitBuilding(BuildingPlacementPreview preview)
    {
        _preview = preview;
        _preview.transform.SetParent(transform);

        // _hub.
    }

    private void OnCollectorAvailabled(Collector collector)
    {

    }
}