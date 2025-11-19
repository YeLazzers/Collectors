using System;
using UnityEngine;

public interface IPoolable<T>
{
    event Action<T> Expired;
    T Initialize(Vector3 position);
}