using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    private List<IObjectPool> poolList = new List<IObjectPool>();

    public ObjectPool<T> GetPool<T>(int _capacity = 10, int increaseSize = 4) where T : IPoolable, new()
    {
        int count = poolList.Count;
        for(int i = 0;i < count; i++)
        {
            if (poolList[i].GetType().Equals(typeof(ObjectPool<T>)))
            {
                Debug.Log("you have in list same pool");
                return (ObjectPool<T>)poolList[i];
            }
        }

        Debug.Log("you don have pool we will create that");
        ObjectPool<T> objectPool = new ObjectPool<T>(_capacity, increaseSize);
        poolList.Add(objectPool);

        return objectPool;
    }
}
