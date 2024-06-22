using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour, ITargetAble
{
    private const float COLLISION_RANGE = 0.3f;
    private const float COLLISION_DLEAY_TIME = 0.2f;
    private const float CREATE_DLEAY_TIME = 1f;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite beforeCreationImage;
    //[SerializeField] private OBBCollision obbController;


    private MonsterInfo monsterInfo;
    private BehaviorLogicBase monsterBehavior;
    private BoxInfo monsterBoxInfo;
    private Sprite monsterSprite;
    private bool isCollision = false;
    private bool isCreate = true;
    private ITargetAble player;
    private float curHP;
    private float createDleayTime = CREATE_DLEAY_TIME;
    //private float invincibleTime = COLLISION_DLEAY_TIME;

    public int monsterIdx;



    private void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        monsterBoxInfo = new BoxInfo();
        spriteRenderer.sprite = beforeCreationImage;
    }

    private void Start()
    {
        player = PlayerManager.getInstance.GetTarget();

    }

    private void Update()
    {

        if (!isCreate)
        {
            createDleayTime -= Time.deltaTime;
            if(createDleayTime < 0)
            {
                isCreate = true;
                isCollision = true;
                createDleayTime = CREATE_DLEAY_TIME;
                spriteRenderer.sprite = monsterSprite;
            }
        }

        monsterBoxInfo.center = this.transform.position;
        //monsterBoxInfo.rot = this.transform.eulerAngles.z;



        if (isCreate)
        {
            //if (!isCollision)
            //{
            //    invincibleTime -= Time.deltaTime;
            //    if (invincibleTime < 0)
            //    {
            //        isCollision = true;
            //        invincibleTime = COLLISION_DLEAY_TIME;
            //        spriteRenderer.color = Color.white;
            //    }
            //}

            monsterBehavior?.Update();


            bool result = OnCollisionAABB();

            if (result)
            {
                Debug.Log("Player Monster Collision");
                player.OnDamege(1);
            }

        }


    }

    public BoxInfo GetBoxInfo()
    {
        return monsterBoxInfo;
    }

    public void SetMonsterInfo(MonsterInfo _info)
    {
        monsterInfo = _info;
        curHP = _info.status[(int)MonsterStatusType.M_HP];
    }

    public void SetMonsterBehavior(BehaviorLogicBase _behavior)
    {
        monsterBehavior = _behavior;
    }

    public void ShowMonster(Vector2 _spwan, Sprite _sprite)
    {
        this.gameObject.SetActive(true);
        this.transform.position = _spwan;
        spriteRenderer.sprite = beforeCreationImage;
        monsterSprite = _sprite;
        monsterBoxInfo.center = _spwan;
        monsterBoxInfo.size = spriteRenderer.bounds.size;
        isCollision = false;
        isCreate = false;

        spriteRenderer.color = Color.white;

    }

    public void DeadMonster()
    {
        this.transform.gameObject.SetActive(false);
        monsterBehavior = null;

        MonsterManager.getInstance.RemoveMonsterList(this);
;
        isCollision = false;
        isCreate = false;

    }


    private bool OnCollisionAABB()
    {
        if (player.IsCollision())
        {
            BoxInfo playerBox = player.GetBoxInfo();

            Vector2 playerCenter = playerBox.center;
            Vector2 monsterCenter = monsterBoxInfo.center;

            float playerWidth = playerBox.size.x;
            float playerHeight = playerBox.size.y;
            float monsterWidth = monsterBoxInfo.size.x;
            float monsterHeight = monsterBoxInfo.size.y;


            if (playerCenter.x - playerWidth * COLLISION_RANGE < monsterCenter.x + monsterWidth * COLLISION_RANGE &&
                playerCenter.x + playerWidth * COLLISION_RANGE > monsterCenter.x - monsterWidth * COLLISION_RANGE &&
                playerCenter.y - playerHeight * COLLISION_RANGE < monsterCenter.y + monsterHeight * COLLISION_RANGE &&
                playerCenter.y + playerHeight * COLLISION_RANGE > monsterCenter.y - monsterHeight * COLLISION_RANGE)
            {
                return true;

            }
        }
        return false;

    }


    public bool IsCollision()
    {
        return isCollision;
    }

    public void OnDamege(float _damageAmount)
    {
        curHP -= _damageAmount;
        //isCollision = false;
        spriteRenderer.color = Color.yellow;

        if (isCreate)
        {
            Vector2 dir = monsterBoxInfo.center - player.GetBoxInfo().center;
            Vector2 monsterPos = this.transform.position;
            monsterPos.x += dir.normalized.x * 0.3f;
            monsterPos.y += dir.normalized.y * 0.3f;
            this.transform.position = monsterPos;
        }

        if (curHP <= 0)
        {
            DeadMonster();
        }
    }
}
