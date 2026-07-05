using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JobBoard : MonoBehaviour
{
    [SerializeField]
    private readonly List<IJob> _jobs = new();

    public event Action<IJob> JobAdded;
    public event Action Changed;

    public void Publish(IJob job)
    {
        _jobs.Add(job);

        JobAdded?.Invoke(job);
        Changed?.Invoke();
    }

    public bool TryGetJob(out IJob job)
    {
        var sortedJobs = _jobs.OrderByDescending(j => j.Priority);
        job = sortedJobs.FirstOrDefault(j => j.Status == JobStatus.Pending);

        if (job != null)
        {
            job.SetStatus(JobStatus.Running);
            return true;
        }
        return false;
    }
}