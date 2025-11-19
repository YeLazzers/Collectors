using System;
using UnityEngine;

[Serializable]
public class ScaleTweenConfig : TweenConfigBase
{
    [Header("Scale Settings")]
    public Vector3 From;
    public bool IsFromCurrent;
    public Vector3 To;
}