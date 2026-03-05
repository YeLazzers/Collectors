using UnityEngine;

public class MeshView : MonoBehaviour
{
    private static readonly int EmissionId = Shader.PropertyToID("_EmissionColor");
    private static readonly int ColorId = Shader.PropertyToID("_Color");

    private Renderer[] _renderers;
    private Material[] _baseMaterials;
    private Color _baseColor;
    private Color _baseEmission;
    private MaterialPropertyBlock _block;

    private Material _currentMaterial;
    private Color? _currentColor;
    private Color? _currentEmission;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>(true);
        _block = new MaterialPropertyBlock();
        InitBaseProperties();
    }

    public void SetEmission(Color color)
    {
        if (_currentEmission == color)
            return;

        _currentEmission = color;

        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].GetPropertyBlock(_block);
            _block.SetColor(EmissionId, color);
            _renderers[i].SetPropertyBlock(_block);
        }
    }

    public void SetEmissionIntensity(float intensity)
    {
        SetEmission(_baseColor * intensity);
    }

    public void ResetEmission()
    {
        SetEmission(_baseEmission);
    }

    public void SetColor(Color color)
    {
        if (_currentColor == color)
            return;

        _currentColor = color;

        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].GetPropertyBlock(_block);
            _block.SetColor(ColorId, color);
            _renderers[i].SetPropertyBlock(_block);
        }
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
        ResetColor();
    }

    public void SetMaterial(Material material)
    {
        if (_currentMaterial == material)
            return;

        _currentMaterial = material;

        foreach (var r in _renderers)
            r.sharedMaterial = material;
    }

    public void ResetMaterial()
    {
        _currentMaterial = null;

        for (int i = 0; i < _renderers.Length; i++)
            _renderers[i].sharedMaterial = _baseMaterials[i];
    }

    public void ResetAll()
    {
        ResetMaterial();
        ResetColor();
        ResetEmission();
    }

    private void InitBaseProperties()
    {
        _baseMaterials = new Material[_renderers.Length];

        for (int i = 0; i < _renderers.Length; i++)
            _baseMaterials[i] = _renderers[i].sharedMaterial;

        if (_renderers.Length == 0 || _renderers[0].sharedMaterial == null)
            return;

        var mat = _renderers[0].sharedMaterial;
        _baseColor = mat.GetColor(ColorId);
        _baseEmission = mat.GetColor(EmissionId);
    }
}
