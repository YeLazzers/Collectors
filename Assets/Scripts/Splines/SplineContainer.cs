using System.Collections.Generic;
using UnityEngine;

public class SplineContainer : MonoBehaviour
{
    private Dictionary<int, SplinePath> _splines = new();

    public SplinePath CreateEmptySpline(int ownerId)
    {
        SplinePath spline = new GameObject($"SplinePath for {ownerId}").AddComponent<SplinePath>();

        _splines[ownerId] = spline;

        return spline;
    }
}