using UnityEngine;

public class Highlighter : MonoBehaviour
{
    [SerializeField] private float _highlightIntensity = 2f;
    [SerializeField] private MeshViewArray _meshViewArray;

    public void Highlight()
    {
        _meshViewArray.SetEmissionIntensity(_highlightIntensity);
    }

    public void Unhighlight()
    {
        _meshViewArray.ResetEmission();
    }
}