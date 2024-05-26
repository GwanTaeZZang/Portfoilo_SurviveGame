using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private OBBCollision obbController;
    private MonsterInfo monsterInfo;
    private BehaviorLogicBase monsterBehavior;

    public int monsterIdx;

    private void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Update()
    {
        monsterBehavior?.Update();
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
        obbController.enabled = true;
        obbController.SetInfo();
        CollisionManager.getInstance.AddMonsterList(obbController);

        Invoke("DeadMonster", 7f);
    }

    public void DeadMonster()
    {
        this.transform.gameObject.SetActive(false);
        monsterBehavior = null;

        MonsterManager.getInstance.RemoveMonsterList(this);
        CollisionManager.getInstance.RemoveMonsterList(obbController);

    }
}
