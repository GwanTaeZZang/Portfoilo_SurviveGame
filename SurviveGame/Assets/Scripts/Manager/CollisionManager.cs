using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : Singleton<CollisionManager>
{
    private LinkedList<OBBCollision> monsterList = new LinkedList<OBBCollision>();
    private OBBCollision playerOBB;

    public void SetPlayer(OBBCollision _playerOBB)
    {
        playerOBB = _playerOBB;
    }
    public void AddMonsterList(OBBCollision _monsterOBB)
    {
        monsterList.AddLast(_monsterOBB);
    }
    public void RemoveMonsterList(OBBCollision _monsterOBB)
    {
        monsterList.Find(_monsterOBB);
    }
    public LinkedList<OBBCollision> GetMonsterList()
    {
        return monsterList;
    }














    public bool IsCollision(BoxInfo _target)
    {
        //.. ???? ???? ???? ???????? ???? ????????
        Vector2 distance = GetCenterDistanceVector(playerOBB.myInfo, _target);
        //Debug.DrawLine(this.transform.position, target.center, Color.red);

        Vector2[] vec = new Vector2[4]
        {
            GetHeightVector(playerOBB.myInfo),
            GetHeightVector(_target),
            GetWidthVector(playerOBB.myInfo),
            GetWidthVector(_target)
        };

        Vector2 unitVec;
        for (int i = 0; i < 4; i++)
        {
            float sum = 0f;
            unitVec = GetUnitVector(vec[i]);
            //Debug.Log("UnitVec = " + unitVec);
            for (int j = 0; j < 4; j++)
            {
                sum += Mathf.Abs(vec[j].x * unitVec.x + vec[j].y * unitVec.y);
            }

            float dotProduct = Mathf.Abs(distance.x * unitVec.x + distance.y * unitVec.y);
            bool isNotCollision = dotProduct >= sum;

            //Debug.Log("sum : " + sum + "  dotProduct : " + dotProduct + "  isNotCollision :" + isNotCollision);

            if (dotProduct >= sum)
            {
                Debug.Log("Dont Collision");

                return false;
            }
        }

        Debug.Log("Collision");
        return true;
    }


    private Vector2 GetCenterDistanceVector(BoxInfo _myInfo ,BoxInfo _target)
    {
        return (_myInfo.center - _target.center);
    }

    private Vector2 GetHeightVector(BoxInfo _box)
    {
        float x = _box.size.y * Mathf.Cos(Deg2Rad(_box.rot - 90f)) / 2;
        float y = _box.size.y * Mathf.Sin(Deg2Rad(_box.rot - 90f)) / 2;

        Debug.DrawLine(_box.center, new Vector2(x + _box.center.x, y + _box.center.y), Color.red);

        return new Vector2(x, y);
    }

    private Vector2 GetWidthVector(BoxInfo _box)
    {
        float x = _box.size.x * Mathf.Cos(Deg2Rad(_box.rot)) / 2;
        float y = _box.size.x * Mathf.Sin(Deg2Rad(_box.rot)) / 2;

        Debug.DrawLine(_box.center, new Vector2(x + _box.center.x, y + _box.center.y), Color.blue);

        return new Vector2(x, y);
    }

    private Vector2 GetUnitVector(Vector2 _v)
    {
        float len = Mathf.Sqrt(Mathf.Pow(_v.x, 2) + Mathf.Pow(_v.y, 2));

        return new Vector2(_v.x / len, _v.y / len);
    }

    private float Deg2Rad(float _deg)
    {
        return _deg / 180 * Mathf.PI;
    }

}
