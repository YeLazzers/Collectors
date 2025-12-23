using UnityEngine;

// public enum ConstructionSiteState
// {
//     Building,
//     Completed
// }

public class BuildingConstructionSite : MonoBehaviour
{
    [SerializeField] private BuildingView _view;

    // private ConstructionSiteState _state;

    public void Initialize(BuildingConfig config)
    {
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