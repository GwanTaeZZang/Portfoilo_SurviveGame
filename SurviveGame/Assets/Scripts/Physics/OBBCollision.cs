using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BoxInfo
{
    public Vector2 center; //.. x,y [Transform Position]
    public Vector2 size; //.. x,y [Image Width / Height]
    public float rot; //.. z?? [Transform Rotation]
}


public class OBBCollision : MonoBehaviour
{
    //public ObbTest target;

    public BoxInfo myInfo = new BoxInfo();
    public SpriteRenderer spriteRenderer;


    private void Awake()
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        myInfo.size = spriteRenderer.bounds.size;
    }


    private void Update()
    {
        myInfo.center = transform.position;
        myInfo.rot = transform.eulerAngles.z;

        //GetHeightVector(myInfo);
        GetHeightVector(myInfo);
        GetWidthVector(myInfo);


        //IsCollisionTest(target.myInfo);
    }

    public void SetInfo()
    {
        myInfo.size = spriteRenderer.bounds.size;
    }

    public bool IsCollisionTest(BoxInfo target)
    {
        //.. ???? ???? ???? ???????? ???? ????????
        Vector2 distance = GetCenterDistanceVector(target);
        Debug.DrawLine(this.transform.position, target.center, Color.red);

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


    private Vector2 GetCenterDistanceVector(BoxInfo target)
    {
        return (myInfo.center - target.center);
    }

    private Vector2 GetHeightVector(BoxInfo box)
    {
        float x = box.size.y * Mathf.Cos(Deg2Rad(box.rot - 90f)) / 2;
        float y = box.size.y * Mathf.Sin(Deg2Rad(box.rot - 90f)) / 2;

        Debug.DrawLine(box.center, new Vector2(x + box.center.x, y + box.center.y), Color.red);

        return new Vector2(x, y);
    }

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
