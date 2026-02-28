using UnityEngine;

// public enum ConstructionSiteState
// {
//     Building,
//     Completed
// }

public class BuildingConstructionSite : MonoBehaviour
{
    [SerializeField] private BuildingView _view;

    private BuildingConfig _config;

    public BuildingConfig Config => _config;
    public Vector3 Position => transform.position;

    public void Initialize(BuildingConfig config)
    {
        _config = config;
        _view.RenderModel(config);
    }

    public void Initialize(BuildingConfig config, Vector3 position)
    {
        Initialize(config);
        SetPosition(position);
    }

    public void CompleteConstruction()
    {
        // _state = ConstructionSiteState.Completed;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}