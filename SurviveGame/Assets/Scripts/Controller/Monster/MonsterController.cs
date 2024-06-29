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
    private const float MONSTER_KNOCKBACK_RANGE = 0.3f;
    private const float MONSTER_HP_HALF = 0.5f;
    private const float MONSTER_HP_20PERCENT = 0.2f;
    private const int JOB_UID_INITIAL_VALUE = 3000;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite beforeCreationImage;
    [SerializeField] private List<RuntimeAnimatorController> animCtrlList;
    [SerializeField] private Animator anim;
    //[SerializeField] private OBBCollision obbController;


    private MonsterInfo monsterInfo;
    private MonsterStatusVariance monsterStatusVariance;
    private MonsterManager monsterMgr;
    private BehaviorLogicBase monsterBehavior;
    private MonsterBehavior moveBehavior;
    private MonsterBehavior attackBehavior;
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
        monsterMgr = MonsterManager.getInstance;
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
                agent.enabled = true;
                int monsterUid = monsterInfo.Uid;
                anim.runtimeAnimatorController = animCtrlList[monsterUid % JOB_UID_INITIAL_VALUE];

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
        if (monsterMgr == null)
        {
            monsterMgr = MonsterManager.getInstance;
        }

        if (monsterInfo == null)
        {
            monsterInfo = new MonsterInfo();
        }

        if(monsterStatusVariance == null)
        {
            monsterStatusVariance = monsterMgr.GetMonsterStatusVariance();
        }

        monsterInfo.Uid = _info.Uid;
        monsterInfo.bootyGold = _info.bootyGold;
        int count = _info.status.Length;
        for(int i =0; i < count; i++)
        {
            monsterInfo.status[i] = _info.status[i] + monsterStatusVariance.status[i];
        }
        monsterInfo.stringKey = _info.stringKey;
        monsterInfo.monsterSpritePath = _info.monsterSpritePath;
        monsterInfo.monsterName = _info.monsterName;
        monsterInfo.logicType = _info.logicType;
        monsterInfo.moveType = _info.moveType;
        monsterInfo.attackType = _info.attackType;

        curHP = monsterInfo.status[(int)MonsterStatusType.M_HP];
    }

    public void SetMonsterBehavior()
    {

        BehaviorLogicBase logic = monsterMgr.GetMonsterbehaviorLogic(monsterInfo.logicType);

        moveBehavior = monsterMgr.GetMonsterMoveBehavior(monsterInfo.moveType);

        attackBehavior = monsterMgr.GetMonsterAttackBehavior(monsterInfo.attackType);

        logic.Initialize(monsterInfo, this.transform, moveBehavior, attackBehavior);

        monsterBehavior = logic;
    }

    //public void SetMonsterBehavior(BehaviorLogicBase _behavior)
    //{
    //    monsterBehavior = _behavior;
    //}

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
        agent.enabled = false;
        spriteRenderer.color = Color.white;

    }

    public void DeadMonster()
    {
        

        monsterMgr.ReleaseMonsterLogicBehavior(monsterBehavior, monsterInfo.logicType);
        monsterMgr.ReleaseMonsterMoveBehavior(moveBehavior, monsterInfo.moveType);
        monsterMgr.ReleaseMonsterAttackBehavior(attackBehavior, monsterInfo.attackType);

        monsterMgr.RemoveMonsterList(this);
        anim.runtimeAnimatorController = null;
        this.transform.position = Vector3.zero;
        this.transform.gameObject.SetActive(false);
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
        float maxHp = monsterInfo.status[(int)MonsterStatusType.M_HP];

        if (maxHp * MONSTER_HP_HALF > curHP)
        {
            spriteRenderer.color = Color.yellow;
        }
        if(maxHp * MONSTER_HP_20PERCENT > curHP)
        {
            spriteRenderer.color = Color.red;
        }

        if (isCreate)
        {
            Vector2 dir = monsterBoxInfo.center - player.GetBoxInfo().center;
            Vector2 monsterPos = this.transform.position;
            monsterPos.x += dir.normalized.x * MONSTER_KNOCKBACK_RANGE;
            monsterPos.y += dir.normalized.y * MONSTER_KNOCKBACK_RANGE;
            this.transform.position = monsterPos;
        }

        if (curHP <= 0)
        {
            GlobalData.getInstance.InCreaseGold(monsterInfo.bootyGold);
            DeadMonster();
        }
    }
}
