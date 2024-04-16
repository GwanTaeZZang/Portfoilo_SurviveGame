using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class TestObject : IPoolable
{
    private Transform model;

    public void Initialize(Transform _model)
    {
        model = GameObject.Instantiate(_model);
    }

    public void SetModel(Transform _model)
    {
        model = GameObject.Instantiate(_model);
    }
    public Transform GetModel()
    {
        if(model == null)
        {
            Debug.Log("not model");
        }
        
        return model;
    }

    public void OnEnqueue()
    {
        model.gameObject.SetActive(false);
    }

    public void OnDequeue()
    {
        model.gameObject.SetActive(true);
        model.position = new Vector3(0, 0, 0);
    }
}
