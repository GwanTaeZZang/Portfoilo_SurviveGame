using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTest1 : MonoBehaviour
{
    [SerializeField] private Transform testModle = null;

    private ObjectPool<TestMonoObject> testPool = null;
    private Queue<TestMonoObject> testObjectQueue = new Queue<TestMonoObject>();

    private GameObject parent;

    private void Start()
    {
        parent = new GameObject();
    }

    private void Update()
    {
        if (Input.GetKeyDown("s"))
        {

            if(testPool == null)
            {
                testPool = ObjectPoolManager.getInstance.GetPool<TestMonoObject>(10);
                testPool.SetModel(testModle, parent.transform);
            }

            Debug.Log("Dequeue Obejct");
            TestMonoObject obj = testPool.Dequeue();
            testObjectQueue.Enqueue(obj);
            obj.OnDequeue();
            obj.ShowMessage("dkdkdkdkdkdkdkkd");
        }
        if (Input.GetKeyDown("a"))
        {
            Debug.Log("Enqueue Object");
            if(testObjectQueue.Count == 0)
            {
                Debug.Log("do not Enqueue");
                return;
            }

            TestMonoObject obj = testObjectQueue.Dequeue();
            obj.OnEnqueue();
            testPool.Enqueue(obj);
        }
    }
}
