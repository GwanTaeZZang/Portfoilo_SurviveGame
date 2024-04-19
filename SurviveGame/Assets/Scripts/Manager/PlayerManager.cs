using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private Dictionary<int, Job> jobDict = new Dictionary<int, Job>();
    private List<Job> jobList = new List<Job>();
    private Job selectedJob;

    public override bool Initialize()
    {
        LoadJobData();
        return true;
    }

    public Job GetValue(int _key)
    {
        if (!jobDict.ContainsKey(_key))
        {
            Debug.Log("have not key");
            return null;
        }

        return jobDict[_key];


        //if (jobDict.TryGetValue(_key, out Job value))
        //{
        //    return value;
        //}
    }

    public List<Job> GetJobList()
    {
        return jobList;
    }

    public void SetSelectedJob(Job _selectedJob)
    {
        selectedJob = _selectedJob;
        Debug.Log("선택된 직업  = " + selectedJob.jobName);
    }

    public Job GetSelectedJob()
    {
        return selectedJob;
    }
    private void LoadJobData()
    {
        JobData jobData = JsonController.ReadJson<JobData>("JobData");
        int count = jobData.jobArr.Length;
        for(int i = 0; i < count; i++)
        {
            Job job = jobData.jobArr[i];
            jobDict.Add(job.Uid, job);
            jobList.Add(job);
        }
    }
}
