using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInfo
{
    public Vector2 center; 
    public Vector2 size; 
    public float rot; 
}


public class OBBCollision : MonoBehaviour
{
    //public ObbTest target;

    private ITargetAble[] targetArr;

    public BoxInfo myInfo = new BoxInfo();
    public SpriteRenderer spriteRenderer;

    public delegate void OnCollisionDelegate(ITargetAble _target);
    public OnCollisionDelegate OnOBBCollisionEvent;

    private List<ITargetAble> collisionTargetList = new List<ITargetAble>();


    private void Update()
    {
        myInfo.center = this.transform.position;
        myInfo.rot = this.transform.eulerAngles.z;


        int count = targetArr.Length;

        for(int i =0; i < count; i++)
        {
            if (targetArr[i].IsCollision() && !collisionTargetList.Contains(targetArr[i]))
            {
                bool result = IsCollisionOBB(targetArr[i].GetBoxInfo());

                if (result)
                {
                    collisionTargetList.Add(targetArr[i]);
                    OnOBBCollisionEvent?.Invoke(targetArr[i]);
                }
            }
        }
    }

    public void SetInfo()
    {
        myInfo.size = spriteRenderer.bounds.size;
        myInfo.center = this.transform.position;
        myInfo.rot = this.transform.eulerAngles.z;
    }

    public void SetObejctSize()
    {
        myInfo.size = spriteRenderer.bounds.size;
    }

    public void SetTarget(params ITargetAble[] _target)
    {
        targetArr = _target;
        collisionTargetList.Clear();
    }

    public void ClearCollisionTargetList()
    {
        collisionTargetList.Clear();
    }

    public bool IsCollisionOBB(BoxInfo _target)
    {
        Vector2 distance = GetCenterDistanceVector(_target);

        Vector2[] vec = new Vector2[4]
        {
            GetHeightVector(myInfo),
            GetHeightVector(_target),
            GetWidthVector(myInfo ),
            GetWidthVector(_target)
        };

        Vector2 unitVec;
        for (int i = 0; i < 4; i++)
        {
            float sum = 0f;
            unitVec = GetUnitVector(vec[i]);
            for (int j = 0; j < 4; j++)
            {
                sum += Mathf.Abs(vec[j].x * unitVec.x + vec[j].y * unitVec.y);
            }

            float dotProduct = Mathf.Abs(distance.x * unitVec.x + distance.y * unitVec.y);
            bool isNotCollision = dotProduct >= sum;


            if (dotProduct >= sum)
            {
                return false;
            }
        }

        return true;
    }


    private Vector2 GetCenterDistanceVector(BoxInfo _target)
    {
        return myInfo.center - _target.center;
    }

    private Vector2 GetHeightVector(BoxInfo _box)
    {
        float x = _box.size.y * Mathf.Cos(Deg2Rad(_box.rot - 90f)) / 2;
        float y = _box.size.y * Mathf.Sin(Deg2Rad(_box.rot - 90f)) / 2;

        return new Vector2(x, y);
    }

    private Vector2 GetWidthVector(BoxInfo _box)
    {
        float x = _box.size.x * Mathf.Cos(Deg2Rad(_box.rot)) / 2;
        float y = _box.size.x * Mathf.Sin(Deg2Rad(_box.rot)) / 2;

        return new Vector2(x, y);
    }

    private Vector2 GetUnitVector(Vector2 _v)
    {
        float len = Mathf.Sqrt(Mathf.Pow(_v.x, 2) + Mathf.Pow(_v.y, 2));

        return new Vector2(_v.x / len, _v.y / len);
    }

    private float Deg2Rad(float deg)
    {
        return deg / 180 * Mathf.PI;
    }


}
