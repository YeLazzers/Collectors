using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HoverHighlighter : MonoBehaviour
{
    private static readonly int EmissionId = Shader.PropertyToID("_EmissionColor");

    [SerializeField] private List<Renderer> _renderers;
    [SerializeField] private float _highlightIntensity = 2f;

    private MaterialPropertyBlock _block;
    private Color _baseEmission;

    private void Awake()
    {
        _block = new MaterialPropertyBlock();
        _baseEmission = _renderers.FirstOrDefault()?.sharedMaterial.GetColor(EmissionId) ?? Color.black;
    }

    public void Highlight()
    {
        foreach (var renderer in _renderers)
        {
            ChangeProperty(renderer, EmissionId, _baseEmission * _highlightIntensity);
        }
    }

    public void Unhighlight()
    {
        foreach (var renderer in _renderers)
        {
            ChangeProperty(renderer, EmissionId, _baseEmission);
        }
    }

    private void ChangeProperty(Renderer renderer, int propertyId, Color color)
    {
        renderer.GetPropertyBlock(_block);
        _block.SetColor(propertyId, color);
        renderer.SetPropertyBlock(_block);
    }
}