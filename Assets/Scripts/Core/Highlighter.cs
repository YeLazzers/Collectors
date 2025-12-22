using UnityEngine;

public class Highlighter : MonoBehaviour
{
    [SerializeField] private float _highlightIntensity = 2f;

    private IMeshView _meshView;

    public void Initialize(IMeshView meshView)
    {
        _meshView = meshView;
    }

    public void Highlight()
    {
        _meshView.SetEmissionIntensity(_highlightIntensity);
    }

    public void Unhighlight()
    {
        _meshView.ResetEmission();
    }
}