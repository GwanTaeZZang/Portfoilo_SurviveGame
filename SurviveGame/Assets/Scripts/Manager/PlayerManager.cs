using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private Dictionary<int, Job> jobDict = new Dictionary<int, Job>();
    private List<Job> jobList = new List<Job>();
    private Job selectedJob;
    private PlayerController player;
    private Character character;
    private ITargetAble target;

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

    public Character GetCharacter()
    {
        character = new Character(selectedJob);
        return character;
    }

    public List<Job> GetJobList()
    {
        return jobList;
    }

    public void SetSelectedJob(Job _selectedJob)
    {
        selectedJob = _selectedJob;
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

    public void SetPlayer(PlayerController _player)
    {
        player = _player;
        target = _player;
    }

    public PlayerController GetPlayer()
    {
        return player;
    }

    public ITargetAble GetTarget()
    {
        return target;
    }
}
