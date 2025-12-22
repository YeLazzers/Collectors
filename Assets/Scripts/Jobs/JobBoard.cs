using System;
using System.Collections.Generic;
using UnityEngine;

public class JobBoard : MonoBehaviour
{
    private readonly List<IJob2> _jobs = new();

    public event Action<IJob2> JobAdded;
    public event Action Changed;

    public void Publish(IJob2 job)
    {
        _jobs.Add(job);
        Debug.Log($"Job published: {job.Name}");
        JobAdded?.Invoke(job);
        Changed?.Invoke();
    }

    public bool TryGetJob(out IJob2 job)
    {
        if (_jobs.Count > 0)
        {
            job = _jobs[0];
            _jobs.RemoveAt(0);
            return true;
        }

        job = null;
        return false;
    }
}