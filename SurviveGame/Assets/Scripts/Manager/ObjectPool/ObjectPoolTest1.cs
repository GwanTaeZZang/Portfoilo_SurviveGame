using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTest1 : MonoBehaviour
{
    [SerializeField] private Transform testModle = null;

    private ObjectPool<TestObject> testPool = null;
    private Queue<TestObject> testObjectQueue = new Queue<TestObject>();

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            if(testPool == null)
            {
                testPool = ObjectPoolManager.Instance.GetPool<TestObject>(10);
                testPool.SetModel(testModle);
            }

            Debug.Log("Dequeue Obejct");
            TestObject obj = testPool.Dequeue();
            testObjectQueue.Enqueue(obj);
            obj.OnDequeue();
        }
        if (Input.GetKeyDown("a"))
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
