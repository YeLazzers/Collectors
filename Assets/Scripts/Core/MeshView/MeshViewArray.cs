using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Wrapper component for managing multiple MeshView components.
/// Provides convenient methods to apply transformations to all meshes at once.
/// </summary>
public class MeshViewArray : MonoBehaviour, IMeshView
{
    [SerializeField] private List<MeshView> _meshViews = new List<MeshView>();

    private void OnEnable()
    {
        if (_meshViews.Count == 0)
            GatherMeshViews();
    }

    [ContextMenu("GatherMeshViews")]
    public void GatherMeshViews()
    {
        _meshViews = GetComponentsInChildren<MeshView>().ToList();
    }

    public void SetEmission(Color color)
    {
        foreach (var meshView in _meshViews)
        {
            meshView.SetEmission(color);
        }
    }

    public void SetEmissionIntensity(float intensity)
    {
        foreach (var meshView in _meshViews)
        {
            meshView.SetEmissionIntensity(intensity);
        }
    }

    public void ResetEmission()
    {
        foreach (var meshView in _meshViews)
        {
            meshView.ResetEmission();
        }
    }

    public void SetColor(Color color)
    {
        foreach (var meshView in _meshViews)
        {
            meshView.SetColor(color);
        }
    }

    public void ResetColor()
    {
        foreach (var meshView in _meshViews)
        {
            meshView.ResetColor();
        }
    }

    public void SetAlpha(float alpha)
    {
        foreach (var meshView in _meshViews)
        {
            meshView.SetAlpha(alpha);
        }
    }

    public void ResetAlpha()
    {
        foreach (var meshView in _meshViews)
        {
            meshView.ResetAlpha();
        }
    }

    public void SetMaterial(Material material)
    {
        foreach (var meshView in _meshViews)
        {
            meshView.SetMaterial(material);
        }
    }

    public void ResetMaterial()
    {
        foreach (var meshView in _meshViews)
        {
            meshView.ResetMaterial();
        }
    }

    public void ResetAll()
    {
        foreach (var meshView in _meshViews)
        {
            meshView.ResetAll();
        }
    }
}
