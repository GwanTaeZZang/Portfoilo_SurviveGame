using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonoObject : MonoBehaviour , IPoolable
{
    private Transform model;


    private void Start()
    {
        Debug.Log("모노 오브젝트가 태어났");
    }

    public void OnDequeue()
    {
        model.gameObject.SetActive(true);
        model.position = new Vector3(0, 0, 0);
    }

    public void OnEnqueue()
    {
        model.gameObject.SetActive(false);
    }

    public void SetModel(Transform _model)
    {
        model = _model;
    }

    public void ShowMessage(string _message)
    {
        Debug.Log(_message);
    }
}
