using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct BoxInfo
{
    public Vector2 center; //.. x,y [Transform Position]
    public Vector2 size; //.. x,y [Image Width / Height]
    public float rot; //.. z�� [Transform Rotation]
}


public class ObbTest : MonoBehaviour
{
    public ObbTest target;

    public BoxInfo myInfo = new BoxInfo();

    private void Awake()
    {
        //.. �̹����� �޾ƿ�
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
        //.. ���� �ǰ� ���� ������ ���� �Ÿ�����
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
                //.. ���� ���� ���밪
                sum += Mathf.Abs(vec[j].x * unitVec.x + vec[j].y * unitVec.y);
            }

            //.. ���� ���� ���밪
            if (Mathf.Abs(distance.x * unitVec.x + distance.y * unitVec.y) > sum)
            {
                Debug.Log("�浹����");

                return false;
            }
        }

        Debug.Log("�浹��");
        return true;
    }

    private Vector2 GetCenterDistanceVector(BoxInfo target)
    {
        return (myInfo.center - target.center);
    }

    //.. ���� ���� (Y��), ȸ�� ���� ���� ���� ���� ���
    private Vector2 GetHeightVector(BoxInfo box)
    {
        float x = box.size.y * Mathf.Cos(Deg2Rad(box.rot - 90f)) / 2;
        float y = box.size.y * Mathf.Sin(Deg2Rad(box.rot - 90f)) / 2;

        return new Vector2(x, y);
    }


    //.. �ʺ� ���� (X��), ȸ�� ���� ���� ���� ���� ���
    private Vector2 GetWidthVector(BoxInfo box)
    {
        float x = box.size.x * Mathf.Cos(Deg2Rad(box.rot)) / 2;
        float y = box.size.x * Mathf.Sin(Deg2Rad(box.rot)) / 2;

        return new Vector2(x, y);
    }

    //.. �������� ����
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
