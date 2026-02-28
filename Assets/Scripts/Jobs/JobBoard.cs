using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JobBoard : MonoBehaviour
{
    private readonly List<IJob> _jobs = new();

    public event Action<IJob> JobAdded;
    public event Action Changed;

    public void Publish(IJob job)
    {
        _jobs.Add(job);
        Debug.Log($"Job published: {job.Name}");
        JobAdded?.Invoke(job);
        Changed?.Invoke();
    }

    public bool TryGetJob(out IJob job)
    {
        var sortedJobs = _jobs.OrderBy(j => j.Priority);
        job = sortedJobs.FirstOrDefault(j => j.Status == JobStatus.Pending);

        if (job != null)
        {
            job.SetStatus(JobStatus.Running);
            return true;
        }
        return false;
    }
}

// https://api.telegram.org/bot8547108805:AAEfTlYpWXC93Vbrc8zlP8JkztH6wkUhEEQ/setWebhook?url=https://yelazzers.app.n8n.cloud/webhook/1a35cc04-3b47-440c-833f-18fc9087c637
// https://api.telegram.org/bot8547108805:AAEfTlYpWXC93Vbrc8zlP8JkztH6wkUhEEQ/getWebhookInfo
