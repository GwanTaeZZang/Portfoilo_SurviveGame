using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterController : MonoBehaviour, ITargetAble
{
    private BossMonsterModel model;
    private BossPatternSelector patternSelector;
    private BossPattern bossPattern;

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

    public void Update()
    {
        bossPattern.UpdateBehaviour();
    }

    public BoxInfo GetBoxInfo()
    {
        throw new System.NotImplementedException();
    }

    public bool IsCollision()
    {
        throw new System.NotImplementedException();
    }

    public void OnDamege(float _damageAmount)
    {
        throw new System.NotImplementedException();
    }
}
