using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonsterController : MonoBehaviour, ITargetAble
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Canvas mainCanvas;

    private BossMonsterModel model;
    private BossPatternSelector patternSelector;
    private BossPattern bossPattern;
    private BoxInfo bossBoxInfo;
    private ITargetAble player;

    private Vector3 prevVector;

    private bool isCollision = false;

    private InGameCanvas ingameCanvas;
    private HpBar hpBar;
    private float curHP;

    private Camera mainCamera;

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
        model = new BossMonsterModel();
        patternSelector = new BossPatternSelector(model, this.transform);
        bossBoxInfo = new BoxInfo();

        mainCamera = Camera.main;
        CreateBossHpBar();

        player = PlayerManager.getInstance.GetTarget();

        patternSelector.CreateBossPattern(model.patternModel.logicType, model.patternModel.behaviourTypeList);

        bossPattern = patternSelector.GetBossPattern();

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

    }

    public void LateUpdate()
    {
        Vector2 moveDir = this.transform.position - prevVector;
        spriteRenderer.flipX = moveDir.x < 0 ? true : false;

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

        //Vector2 dir = bossBoxInfo.center - player.GetBoxInfo().center;
        //Vector2 monsterPos = this.transform.position;
        //monsterPos.x += dir.normalized.x * 0.05f;
        //monsterPos.y += dir.normalized.y * 0.05f;
        //this.transform.position = monsterPos;
    }
}
