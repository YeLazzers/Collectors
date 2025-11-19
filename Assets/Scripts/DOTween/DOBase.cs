using DG.Tweening;
using UnityEngine;

public abstract class DOBase : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] protected bool PlayOnStart = false;
    [SerializeField] protected float Duration;
    [SerializeField] protected int Repeats = -1;
    [SerializeField] protected LoopType LoopType;
    [SerializeField] protected Ease EaseType;

    public abstract void Play();

    public virtual void Start()
    {
        if (PlayOnStart)
            Play();
    }
}