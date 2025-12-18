using UnityEngine;

/// <summary>
/// Component for managing material transformations on a single Renderer.
/// Uses MaterialPropertyBlock for efficient material property changes.
/// Allows brightness, transparency, and other material modifications.
/// </summary>
[RequireComponent(typeof(Renderer))]
public class MeshView : MonoBehaviour, IMeshView
{
    private static readonly int EmissionId = Shader.PropertyToID("_EmissionColor");
    private static readonly int ColorId = Shader.PropertyToID("_Color");

    private Renderer _renderer;
    private MaterialPropertyBlock _block;
    private Color _baseEmissionColor;
    private Color _baseColor;
    private float _baseAlpha;
    private Material _baseMaterial;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        _block = new MaterialPropertyBlock();
        CacheBaseProperties();
    }

    public void SetEmission(Color color)
    {
        _renderer.GetPropertyBlock(_block);
        _block.SetColor(EmissionId, color);
        _renderer.SetPropertyBlock(_block);
    }

    public void SetEmissionIntensity(float intensity)
    {
        SetEmission(_baseEmissionColor * intensity);
    }

    public void ResetEmission()
    {
        SetEmission(_baseEmissionColor);
    }

    public void SetColor(Color color)
    {
        _renderer.GetPropertyBlock(_block);
        _block.SetColor(ColorId, color);
        _renderer.SetPropertyBlock(_block);
    }

    public void ResetColor()
    {
        SetColor(_baseColor);
    }

    public void SetAlpha(float alpha)
    {
        Color color = _baseColor;
        color.a = Mathf.Clamp01(alpha);
        SetColor(color);
    }

    public void ResetAlpha()
    {
        SetAlpha(_baseAlpha);
    }

    public void SetMaterial(Material material)
    {
        _renderer.sharedMaterial = material;
    }

    public void ResetMaterial()
    {
        SetMaterial(_baseMaterial);
    }

    public void ResetAll()
    {
        ResetEmission();
        SetColor(_baseColor);
        SetMaterial(_baseMaterial);
    }

    private void CacheBaseProperties()
    {
        if (_renderer == null || _renderer.sharedMaterial == null)
            return;

        _baseMaterial = _renderer.sharedMaterial;

        _baseEmissionColor = _baseMaterial.GetColor(EmissionId);
        _baseColor = _baseMaterial.GetColor(ColorId);
        _baseAlpha = _baseColor.a;
    }
}