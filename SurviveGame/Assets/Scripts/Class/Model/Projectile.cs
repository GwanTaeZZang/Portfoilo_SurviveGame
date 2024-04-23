using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : IPoolable
{
    private Transform model;

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
        model = GameObject.Instantiate(_model);
    }
}