using TMPro;
using UnityEngine;

public class SelectionPanel : MonoBehaviour
{
    [SerializeField] private BuildingSelectionView _buildingSelectionView;
    [SerializeField] private TextMeshProUGUI _titleText;

    public void UpdateStats(IBuildingReadModel model)
    {
        _buildingSelectionView.RenderStats(model);
    }

    public void SetTitle(string title)
    {
        _titleText.text = title;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Show(string title)
    {
        SetTitle(title);
        Show();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}