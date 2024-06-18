using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour , ITargetAble
{
    private const float HALF = 0.5f;

    [SerializeField] SpriteRenderer playerSpriteRenderer;
    [SerializeField] JoyPad2DController joyPad;
    [SerializeField] GameObject hpBar;
    [SerializeField] Image hpImage;

    private ITargetAble[] targetArr;
    private BoxInfo playerBoxInfo;
    private Character character;
    //private float speed = 3;
    private bool isCollision = true;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;

        joyPad.Initialize(OnMove);

        PlayerManager.getInstance.SetPlayer(this);

        playerBoxInfo = new BoxInfo();


    }
    public void Initialize()
    {
        character = PlayerManager.getInstance.GetCharacter();
        //character.job = PlayerManager.getInstance.GetJobList()[0];
        SetPlayerSprite(Resources.Load<Sprite>(character.job.jobSpritePath));

        playerBoxInfo.size = playerSpriteRenderer.bounds.size;

        hpBar.transform.position = mainCamera.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 0.55f));
    }

    private void Update()
    {
        bool result = OnCollisionAABB();

        if (result)
        {
            //Debug.Log("Collision");
        }
        else
        {
            //Debug.Log("Not Collision");
        }
    }

    private void OnMove(Vector2 _dir)
    {
        if (_dir == Vector2.zero)
            return;

        float speed = character.statusArr[(int)StatusEffectType.P_Speed].status;


        Vector3 curPos = this.transform.position;
        curPos.x += _dir.x * speed * Time.deltaTime;
        curPos.y += _dir.y * speed * Time.deltaTime;
        this.transform.position = curPos;
        playerBoxInfo.center = curPos;

        playerSpriteRenderer.flipX = _dir.x < 0;

        curPos.y += 1;
        hpBar.transform.position = mainCamera.WorldToScreenPoint(curPos);
    }

    private void SetPlayerSprite(Sprite _sprite)
    {
        playerSpriteRenderer.sprite = _sprite;
    }



    private bool OnCollisionAABB()
    {

        if(targetArr == null)
        {
            targetArr = MonsterManager.getInstance.GetTargetArr();
        }

        int count = targetArr.Length;

        for(int i =0; i < count; i++)
        {
            if (targetArr[i].IsCollision())
            {
                BoxInfo monsterBox = targetArr[i].GetBoxInfo();

                Vector2 playerCenter = playerBoxInfo.center;
                Vector2 monsterCenter = monsterBox.center;

                float playerWidth = playerBoxInfo.size.x;
                float playerHeight = playerBoxInfo.size.y;
                float monsterWidth = monsterBox.size.x;
                float monsterHeight = monsterBox.size.y;


                if (playerCenter.x - playerWidth * HALF < monsterCenter.x + monsterWidth * HALF &&
                    playerCenter.x + playerWidth * HALF > monsterCenter.x - monsterWidth * HALF &&
                    playerCenter.y - playerHeight * HALF < monsterCenter.y + monsterHeight * HALF &&
                    playerCenter.y + playerHeight * HALF > monsterCenter.y - monsterHeight * HALF)
                {
                    return true;
                }
            }

        }
        return false;

    }

    public bool IsCollision()
    {
        return isCollision;
    }

    public BoxInfo GetBoxInfo()
    {
        return playerBoxInfo;
    }

    public void OnDamege()
    {
        Debug.Log("Ums");
    }
}
