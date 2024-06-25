using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterController : MonoBehaviour, ITargetAble
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private BossMonsterModel model;
    private BossPatternSelector patternSelector;
    private BossPattern bossPattern;
    private BoxInfo bossBoxInfo;
    private ITargetAble player;

    private Vector3 prevVector;

    private bool isCollision = false;

    private void Awake()
    {
        model = new BossMonsterModel();
        patternSelector = new BossPatternSelector(model, this.transform);
    }

    private void Start()
    {
        patternSelector.CreateBossPattern(model.patternModel.logicType, model.patternModel.behaviourTypeList);

        bossPattern = patternSelector.GetBossPattern();
    }

    public void Initialized()
    {
        model = new BossMonsterModel();
        patternSelector = new BossPatternSelector(model, this.transform);
        bossBoxInfo = new BoxInfo();

        player = PlayerManager.getInstance.GetTarget();

        patternSelector.CreateBossPattern(model.patternModel.logicType, model.patternModel.behaviourTypeList);

        bossPattern = patternSelector.GetBossPattern();

    }

    public void ShowBossMonster(Vector2 _spwan)
    {
        this.transform.position = _spwan;
        bossBoxInfo.center = _spwan;
        bossBoxInfo.size = spriteRenderer.bounds.size;
        isCollision = true;
        this.gameObject.SetActive(true);
    }

    public void Update()
    {
        if (isCollision)
        {

        }


        //spriteRenderer.flipX = player.GetBoxInfo().center.x < this.transform.position.x;

        bossBoxInfo.center = this.transform.position;
        prevVector = this.transform.position;

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
        //Vector2 dir = bossBoxInfo.center - player.GetBoxInfo().center;
        //Vector2 monsterPos = this.transform.position;
        //monsterPos.x += dir.normalized.x * 0.1f;
        //monsterPos.y += dir.normalized.y * 0.1f;
        //this.transform.position = monsterPos;
    }
}
