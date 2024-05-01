using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTest : MonoBehaviour
{
    [SerializeField] private Transform testModle = null;

    private ObjectPool<TestObject> testPool;
    private Queue<TestObject> testObjectQueue = new Queue<TestObject>();

    private void Start()
    {
        GameObject parent = new GameObject();

        testPool = ObjectPoolManager.getInstance.GetPool<TestObject>(10);
        testPool.SetModel(testModle, parent.transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            Debug.Log("Dequeue Obejct");
            TestObject obj = testPool.Dequeue();
            testObjectQueue.Enqueue(obj);
            obj.OnDequeue();
        }
        if (Input.GetKeyDown("q"))
        {
            Debug.Log("Enqueue Object");
            if(testObjectQueue.Count == 0)
            {
                Debug.Log("do not Enqueue");
                return;
            }

            TestObject obj = testObjectQueue.Dequeue();
            obj.OnEnqueue();
            testPool.Enqueue(obj);
        }
    }
}
