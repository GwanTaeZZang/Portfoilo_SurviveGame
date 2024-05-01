using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour , IPoolable
{
    private MonsterInfo monsterInfo;
    private BehaviorLogicBase monsterBehavior;
    private Transform model;

    public int monsterIdx;


    private void Update()
    {
        monsterBehavior?.Update();
    }

    public void OnDequeue()
    {
        model.gameObject.SetActive(true);
        model.position = new Vector3(5, 5, 0);
    }

    public void OnEnqueue()
    {
        model.gameObject.SetActive(false);
    }

    public void SetModel(Transform _model)
    {
        model = _model;
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
        model.gameObject.SetActive(true);
        model.position = new Vector3(5, 5, 0);
    }

    public void HideMonster()
    {
        model.gameObject.SetActive(false);
        monsterBehavior = null;
    }
}
