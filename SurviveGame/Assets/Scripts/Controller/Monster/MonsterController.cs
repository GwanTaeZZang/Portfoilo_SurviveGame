using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private MonsterInfo monsterInfo;
    private BehaviorLogicBase monsterBehavior;

    public int monsterIdx;


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

    public void ShowMonster()
    {
        this.gameObject.SetActive(true);
        this.transform.position = new Vector3(5, 5, 0);
    }

    public void HideMonster()
    {
        this.transform.gameObject.SetActive(false);
        monsterBehavior = null;
    }
}
