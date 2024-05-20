using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct BoxInfo
{
    public Vector2 center; //.. x,y [Transform Position]
    public Vector2 size; //.. x,y [Image Width / Height]
    public float rot; //.. z축 [Transform Rotation]
}


public class ObbTest : MonoBehaviour
{
    public ObbTest target;

    public BoxInfo myInfo = new BoxInfo();

    private void Awake()
    {
        //.. 이미지꺼 받아와
        myInfo.size = new Vector2(100f, 100f);
    }

    private void Update()
    {
        myInfo.center = transform.localPosition;
        myInfo.rot = transform.eulerAngles.z;

        IsCollisionTest(target.myInfo);
    }

    public bool IsCollisionTest(BoxInfo target)
    {
        //.. 나와 피격 검출 대상과의 중점 거리벡터
        Vector2 distance = GetCenterDistanceVector(target);
        Vector2[] vec = new Vector2[4]
        {
            GetHeightVector(myInfo),
            GetHeightVector(target),
            GetWidthVector(myInfo ),
            GetWidthVector(target)
        };

        Vector2 unitVec;
        for (int i = 0; i < 4; i++)
        {
            float sum = 0f;
            unitVec = GetUnitVector(vec[i]);
            for (int j = 0; j < 4; j++)
            {
                //.. 벡터 내적 절대값
                sum += Mathf.Abs(vec[j].x * unitVec.x + vec[j].y * unitVec.y);
            }

            //.. 벡터 내적 절대값
            if (Mathf.Abs(distance.x * unitVec.x + distance.y * unitVec.y) > sum)
            {
                Debug.Log("충돌안해");

                return false;
            }
        }

        Debug.Log("충돌해");
        return true;
    }

    private Vector2 GetCenterDistanceVector(BoxInfo target)
    {
        return (myInfo.center - target.center);
    }

    //.. 높이 벡터 (Y축), 회전 값에 따른 벡터 방향 계산
    private Vector2 GetHeightVector(BoxInfo box)
    {
        float x = box.size.y * Mathf.Cos(Deg2Rad(box.rot - 90f)) / 2;
        float y = box.size.y * Mathf.Sin(Deg2Rad(box.rot - 90f)) / 2;

        return new Vector2(x, y);
    }


    //.. 너비 벡터 (X축), 회전 값에 따른 벡터 방향 계산
    private Vector2 GetWidthVector(BoxInfo box)
    {
        float x = box.size.x * Mathf.Cos(Deg2Rad(box.rot)) / 2;
        float y = box.size.x * Mathf.Sin(Deg2Rad(box.rot)) / 2;

        return new Vector2(x, y);
    }

    //.. 단위벡터 연산
    private Vector2 GetUnitVector(Vector2 v)
    {
        float len = Mathf.Sqrt(Mathf.Pow(v.x, 2) + Mathf.Pow(v.y, 2));

        return new Vector2(v.x / len, v.y / len);
    }

    private float Deg2Rad(float deg)
    {
        return deg / 180 * Mathf.PI;
    }
}
