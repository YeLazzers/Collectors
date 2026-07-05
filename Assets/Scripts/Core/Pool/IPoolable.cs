using System;
using UnityEngine;

public interface IPoolable<T>
{
    T Initialize(Vector3 position);
}