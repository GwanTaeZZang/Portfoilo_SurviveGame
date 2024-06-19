using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour, ITargetAble
{
    private const float COLLISION_RANGE = 0.3f;
    private const float COLLISION_DLEAY_TIME = 0.2f;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SpriteRenderer spriteRenderer;
    //[SerializeField] private OBBCollision obbController;


    private MonsterInfo monsterInfo;
    private BehaviorLogicBase monsterBehavior;
    private BoxInfo monsterBoxInfo;
    private bool isCollision = false;
    private ITargetAble player;
    private float curHP;
    private float invincibleTime = COLLISION_DLEAY_TIME;

    public int monsterIdx;



    private void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        monsterBoxInfo = new BoxInfo();

    }

    private void Start()
    {
        player = PlayerManager.getInstance.GetTarget();
    }

    private void Update()
    {
        if (!isCollision)
        {
            invincibleTime -= Time.deltaTime;
            if(invincibleTime < 0)
            {
                isCollision = true;
                invincibleTime = COLLISION_DLEAY_TIME;
                spriteRenderer.color = Color.white;
            }
        }

        monsterBoxInfo.center = this.transform.position;
        //monsterBoxInfo.rot = this.transform.eulerAngles.z;

        monsterBehavior?.Update();

        bool result = OnCollisionAABB();

        if (result)
        {
            Debug.Log("Player Monster Collision");
            player.OnDamege(1);
        }

    }

    public BoxInfo GetBoxInfo()
    {
        return monsterBoxInfo;
    }

    public void SetMonsterInfo(MonsterInfo _info)
    {
        monsterInfo = _info;
        curHP = _info.status[(int)MonsterStatus.M_HP];
    }

    public void SetMonsterBehavior(BehaviorLogicBase _behavior)
    {
        monsterBehavior = _behavior;
    }

    public void ShowMonster(Vector2 _spwan, Sprite _sprite)
    {
        this.gameObject.SetActive(true);
        this.transform.position = _spwan;
        spriteRenderer.sprite = _sprite;

        monsterBoxInfo.center = _spwan;
        monsterBoxInfo.size = spriteRenderer.bounds.size;
        isCollision = true;


        spriteRenderer.color = Color.white;

    }

    public void DeadMonster()
    {
        this.transform.gameObject.SetActive(false);
        monsterBehavior = null;

        MonsterManager.getInstance.RemoveMonsterList(this);
;
        isCollision = false;

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
        isCollision = false;
        spriteRenderer.color = Color.yellow;


        if (curHP <= 0)
        {
            DeadMonster();
        }
    }
}
