using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IObjectPool where T : IPoolable, new()
{
    private Queue<T> pool;
    private Transform model = null;
    int increaseSize; // defult = 4

    public ObjectPool(int _capacity, int _increaseSize)
    {
        pool = new Queue<T>(_capacity);
        increaseSize = _increaseSize;
    }
    public void SetModel(Transform _model)
    {
        model = _model;
    }
    public T Dequeue()
    {
        if(pool.Count < 1)
        {
            Debug.Log("풀에 있는 새로운 객체 생;");
            for(int i =0; i< increaseSize; i++)
            {
                T obj = new T();
                if (model != null)
                {
                    obj.SetModel(model);
                }
                Enqueue(obj);
            }
        }
        return pool.Dequeue();
    }
    public void Enqueue(T t)
    {
        pool.Enqueue(t);
    }
}
