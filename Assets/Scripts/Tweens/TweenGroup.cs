using System.Collections.Generic;
using UnityEngine;

public class TweenGroup : MonoBehaviour
{
    [SerializeField] private List<TweenPreset> _presets;

    private void Awake()
    {
        foreach (var preset in _presets)
        {
            Debug.Log($"Preset: {preset.name.ToString()}");
        }
    }
}