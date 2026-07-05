using UnityEngine;
using YeLazzers.Buildings.Modules;

[RequireComponent(typeof(WorkerJobRunner))]
public class Worker : MonoBehaviour, IPoolable<Worker>
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private WorkerHub _hub;
    private WorkerJobRunner _jobRunner;

    public float Speed => _speed;

    public float RotationSpeed => _rotationSpeed;

    public WorkerHub Hub => _hub;

    private void Awake()
    {
        name = $"{name} {GetInstanceID()}";
        _jobRunner = GetComponent<WorkerJobRunner>();
    }

    public Worker Initialize(Vector3 position)
    {
        transform.position = position;
        return this;
    }

    public Worker Initialize(Vector3 position, Vector3 direction)
    {
        transform.SetPositionAndRotation(position, Quaternion.LookRotation(direction - position));
        return this;
    }

    public void AssignToWorkerHub(WorkerHub hub)
    {
        _hub?.RemoveWorker(this);
        _hub = hub;

        _jobRunner.SetJobBoard(hub.JobBoard);
        _jobRunner.Run();
    }
}
