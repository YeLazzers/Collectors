using UnityEngine;

public interface IMeshView
{
    void SetEmission(Color color);
    void SetEmissionIntensity(float intensity);
    void ResetEmission();
    void SetColor(Color color);
    void ResetColor();
    void SetAlpha(float alpha);
    void ResetAlpha();
    void SetMaterial(Material material);
    void ResetMaterial();
    void ResetAll();
}