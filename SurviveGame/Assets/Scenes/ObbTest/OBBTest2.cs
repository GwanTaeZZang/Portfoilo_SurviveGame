using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBBTest : MonoBehaviour
{
    public BoxInfo myInfo = new BoxInfo();
    private Vector2 tempVector = new Vector2(3, 0);
    private SpriteRenderer spriteRenderer;
    private float e;
    private void Awake()
    {
        //.. ???????? ??????
        spriteRenderer = GetComponent<SpriteRenderer>();
        myInfo.size = spriteRenderer.bounds.size;
    }

    // Update is called once per frame
    private void Update()
    {
        myInfo.center = transform.localPosition;
        myInfo.rot = transform.eulerAngles.z;

        GetHeightVector(myInfo);
        GetWidthVector(myInfo);

        Debug.DrawLine(Vector2.zero, tempVector, Color.red);

        if (Input.GetKey("space"))
        {
            TestInnerProduct();
        }

        Vector2 widthUnitVector = GetUnitVector(GetWidthVector(myInfo));

        e = Mathf.Abs(widthUnitVector.x * tempVector.x + widthUnitVector.y * tempVector.y);

        Vector2 endVector = tempVector.normalized;
        endVector.x *= e;
        endVector.y *= e;
        Debug.Log(e);
        Debug.DrawLine(Vector2.zero, endVector, Color.black);


    }

    public void TestInnerProduct()
    {
        Vector2 heightUnitVector = GetUnitVector(GetHeightVector(myInfo));
        Vector2 widthUnitVector = GetUnitVector(GetWidthVector(myInfo));

        float a = Mathf.Abs(heightUnitVector.x * GetHeightVector(myInfo).x + heightUnitVector.y * GetHeightVector(myInfo).y);
        Debug.Log("???? ?????????? ?????????? ????" + a);

        float b = Mathf.Abs(heightUnitVector.x * GetWidthVector(myInfo).x + heightUnitVector.y * GetWidthVector(myInfo).y);
        Debug.Log("???? ?????????? ?????????? ????" + b);

        float c = Mathf.Abs(widthUnitVector.x * GetHeightVector(myInfo).x + widthUnitVector.y * GetHeightVector(myInfo).y);
        Debug.Log("???? ?????????? ?????????? ????" + c);

        float d = Mathf.Abs(widthUnitVector.x * GetWidthVector(myInfo).x + widthUnitVector.y * GetWidthVector(myInfo).y);
        Debug.Log("???? ?????????? ?????????? ????" + d);


        e = Mathf.Abs(widthUnitVector.x * tempVector.x + widthUnitVector.y * tempVector.y);
        Debug.Log("???? ?????????? ?????????? ????" + e);

    }


    public bool IsCollisionTest(BoxInfo target)
    {
        //.. ???? ???? ???? ???????? ???? ????????
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
                //.. ???? ???? ??????
                sum += Mathf.Abs(vec[j].x * unitVec.x + vec[j].y * unitVec.y);
            }

            //.. ???? ???? ??????
            if (Mathf.Abs(distance.x * unitVec.x + distance.y * unitVec.y) > sum)
            {
                Debug.Log("????????");

                return false;
            }
        }

        Debug.Log("??????");
        return true;
    }

    private Vector2 GetCenterDistanceVector(BoxInfo target)
    {
        return (myInfo.center - target.center);
    }



    private Vector2 GetHeightVector(BoxInfo box)
    {
        float x = box.size.y * Mathf.Cos(Deg2Rad(box.rot - 90f)) / 2;
        float y = box.size.y * Mathf.Sin(Deg2Rad(box.rot - 90f)) / 2;

        Debug.DrawLine(box.center, new Vector2(x + box.center.x, y + box.center.y), Color.green);

        return new Vector2(x, y);
    }


    //.. ???? ???? (X??), ???? ???? ???? ???? ???? ????
    private Vector2 GetWidthVector(BoxInfo box)
    {
        float x = box.size.x * Mathf.Cos(Deg2Rad(box.rot)) / 2;
        float y = box.size.x * Mathf.Sin(Deg2Rad(box.rot)) / 2;

        Debug.DrawLine(box.center, new Vector2(x + box.center.x, y + box.center.y), Color.blue);

        return new Vector2(x, y);
    }


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
