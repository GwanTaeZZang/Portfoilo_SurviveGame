using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    public void SetModel(Transform _model);
    public void OnEnqueue();
    public void OnDequeue();
}
public interface IObjectPool
{

}