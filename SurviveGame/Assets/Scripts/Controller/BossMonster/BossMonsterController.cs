using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossMonsterController : MonoBehaviour, ITargetAble
{
    private const float COLLISION_RANGE = 0.4f;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Animator anim;

    private BossMonsterModel model;
    private BossPatternSelector patternSelector;
    private BossPattern bossPattern;
    private BoxInfo bossBoxInfo;
    private ITargetAble player;

    private Vector3 prevVector;

    private bool isCollision = false;

    //private InGameCanvas ingameCanvas;
    private HpBar hpBar;
    private float curHP;

    private Camera mainCamera;

    private int totalPhase;
    private int curPhase;

    private void Awake()
    {
        //model = new BossMonsterModel();
        //patternSelector = new BossPatternSelector(model, this.transform);
    }

    private void Start()
    {
        //patternSelector.CreateBossPattern(model.patternModel.logicType, model.patternModel.behaviourTypeList);

        //bossPattern = patternSelector.GetBossPattern();
    }

    public void Initialized()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        isCollision = false;
        model = new BossMonsterModel();
        patternSelector = new BossPatternSelector(model, this.transform);
        bossBoxInfo = new BoxInfo();

        mainCamera = Camera.main;
        CreateBossHpBar();

        player = PlayerManager.getInstance.GetTarget();

        patternSelector.CreateBossPattern(model.patternModelArr);
        totalPhase = model.patternModelArr.Length;
        curPhase = 0;

        bossPattern = patternSelector.GetBossPattern(curPhase);
    }

    public void CreateBossHpBar()
    {
        hpBar = Instantiate<HpBar>(Resources.Load<HpBar>("Prefabs/BossHPBar"));
    }

    public void ShowBossMonster(Vector2 _spwan, InGameCanvas _canvas)
    {
        this.transform.position = _spwan;
        bossBoxInfo.center = _spwan;
        bossBoxInfo.size = spriteRenderer.bounds.size;

        isCollision = true;

        hpBar.transform.SetParent(_canvas.transform);
        hpBar.gameObject.SetActive(true);

        curHP = model.hp;

        this.gameObject.SetActive(true);
    }

    public void Update()
    {
        if (isCollision)
        {

        }

        Vector2 bossPos = this.transform.position;

        bossBoxInfo.center = bossPos;
        prevVector = bossPos;

        bossPos.y += 1f;

        hpBar.transform.position = mainCamera.WorldToScreenPoint(bossPos);

        bossPattern.UpdateBehaviour();


        bool result = OnCollisionAABB();
        if (result)
        {
            player.OnDamege(model.damage);
        }
    }

    public void LateUpdate()
    {
        Vector2 moveDir = this.transform.position - prevVector;
        spriteRenderer.flipX = moveDir.x < 0 ? true : false;

        //Debug.Log(moveDir.magnitude);
        anim.SetFloat("Move", moveDir.normalized.magnitude);

    }

    private bool OnCollisionAABB()
    {
        if (player.IsCollision())
        {
            BoxInfo playerBox = player.GetBoxInfo();

            Vector2 playerCenter = playerBox.center;
            Vector2 monsterCenter = bossBoxInfo.center;

            float playerWidth = playerBox.size.x;
            float playerHeight = playerBox.size.y;
            float monsterWidth = bossBoxInfo.size.x;
            float monsterHeight = bossBoxInfo.size.y;


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


    public void DeadBossMonster()
    {
        Debug.Log("MonsterDead");
        this.gameObject.SetActive(false);
        hpBar.gameObject.SetActive(false);
        isCollision = false;

    }

    public BoxInfo GetBoxInfo()
    {
        return bossBoxInfo;
    }

    public bool IsCollision()
    {
        return isCollision;
    }

    public void OnDamege(float _damageAmount)
    {
        curHP -= _damageAmount;
        float fillAmount = curHP / model.hp;
        hpBar.ShowHpIamgeFillAmount(fillAmount);


        if(curHP < 0)
        {
            DeadBossMonster();
            return;
        }

        float changePhaseHp = (model.hp - ((model.hp / totalPhase) * (curPhase + 1)));
        float phaseHp = changePhaseHp / model.hp;


        //Debug.Log(fillAmount + "<" + phaseHp);
        if(fillAmount < phaseHp && curPhase < totalPhase)
        {
            curPhase++;
            bossPattern = patternSelector.GetBossPattern(curPhase);
        }

        //Vector2 dir = bossBoxInfo.center - player.GetBoxInfo().center;
        //Vector2 monsterPos = this.transform.position;
        //monsterPos.x += dir.normalized.x * 0.05f;
        //monsterPos.y += dir.normalized.y * 0.05f;
        //this.transform.position = monsterPos;
    }
}
