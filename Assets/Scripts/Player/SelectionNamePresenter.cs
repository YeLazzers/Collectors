using TMPro;
using UnityEngine;

public class SelectionNamePresenter : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private SelectingMode _selector;
    [SerializeField] private TextMeshProUGUI _nameLabel;
    [SerializeField] private Vector3 _labelOffset;

    private void OnEnable()
    {
        _selector.Hovered += OnHovered;
    }

    private void OnDisable()
    {
        _selector.Hovered -= OnHovered;
    }

    private void UpdatePosition(Vector3 mousePosition)
    {
        _nameLabel.rectTransform.position = mousePosition + _labelOffset;
    }

    private void OnHovered(IHoverable hoverable)
    {
        if (hoverable != null)
        {
            _nameLabel.text = hoverable.Name;
            _inputReader.MouseMoved += UpdatePosition;

            _nameLabel.gameObject.SetActive(true);
        }
        else
        {
            _inputReader.MouseMoved -= UpdatePosition;

            _nameLabel.gameObject.SetActive(false);
        }
    }
}