using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class ValueRow : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _amountText;

    public void Initialize(Sprite icon, int amount)
    {
        SetIcon(icon);
        SetAmount(amount);
    }

    public void SetAmount(int amount)
    {
        _amountText.text = amount.ToString();
    }

    public void SetIcon(Sprite icon)
    {
        _icon.sprite = icon;
    }

}