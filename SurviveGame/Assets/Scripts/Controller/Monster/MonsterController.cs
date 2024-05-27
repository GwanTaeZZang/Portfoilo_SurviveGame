using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour, ITargetAble
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SpriteRenderer spriteRenderer;
    //[SerializeField] private OBBCollision obbController;
    private MonsterInfo monsterInfo;
    private BehaviorLogicBase monsterBehavior;
    private BoxInfo monsterBoxInfo;
    private bool isCollision = false;

    public int monsterIdx;

    private void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        monsterBoxInfo = new BoxInfo();

    }
    private void Update()
    {
        monsterBoxInfo.center = this.transform.position;
        //monsterBoxInfo.rot = this.transform.eulerAngles.z;

        monsterBehavior?.Update();
    }

    public BoxInfo GetBoxInfo()
    {
        return monsterBoxInfo;
    }

    public void SetMonsterInfo(MonsterInfo _info)
    {
        monsterInfo = _info;
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
    }

    public void DeadMonster()
    {
        this.transform.gameObject.SetActive(false);
        monsterBehavior = null;

        MonsterManager.getInstance.RemoveMonsterList(this);
;
        isCollision = false;

    }

    public bool IsCollision()
    {
        return isCollision;
    }

    public void OnDamege()
    {
        DeadMonster();
    }
}
