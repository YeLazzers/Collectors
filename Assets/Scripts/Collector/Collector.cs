using UnityEngine;

public class Collector : MonoBehaviour, ICollectWorkable
{
    [SerializeField] private CollectorBrain _brain;

    [SerializeField] private float _movementSpeed;


    private CollectJob _collectJob;
    public CollectJob CurrentJob => _collectJob;
    public float Speed => _movementSpeed;

    private void Awake()
    {
        name = $"{name} {GetInstanceID()}";
    }

    public void SetJob(IJob job)
    {
        job.ApplyTo(this);
    }

    public void ClearJob()
    {
        _collectJob = null;
    }

    public void BeginCollect(CollectJob job)
    {
        Debug.Log($"Collector {GetInstanceID()} got job");
        _collectJob = job;

        _brain.BeginCollect();
    }
}