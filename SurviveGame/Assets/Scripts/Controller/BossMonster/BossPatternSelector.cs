using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternSelector
{
    private List<BossPattern> bossPatternList;

    private IBehaviour[] bossBehaviourArr;

    private BossMonsterModel model;
    private Transform bossTransform;

    public BossPatternSelector(BossMonsterModel _model, Transform _bossTransform)
    {
        model = _model;
        bossTransform = _bossTransform;
        bossPatternList = new List<BossPattern>();

        BindBossBehaviourInstance();
    }

    public BossPattern GetBossPattern()
    {
        return bossPatternList[0];
    }

    private void BindBossBehaviourInstance()
    {
        DashToTarget dashToTarget = new DashToTarget();
        BossApproachToTarget bossApproachToTarget = new BossApproachToTarget();
        RandomDash randomDash = new RandomDash();
        BackRush backRush = new BackRush();
        HexagonShoot hexagonShoot = new HexagonShoot();
        ContinuousHexagonShoot continuousHexagonShoot = new ContinuousHexagonShoot();
        TurningShoot turningShoot = new TurningShoot();

        bossBehaviourArr = new IBehaviour[(int)BossBehaviourType.End]
        {
            dashToTarget,
            bossApproachToTarget,
            randomDash,
            backRush,
            hexagonShoot,
            continuousHexagonShoot,
            turningShoot
        };
    }

    public void CreateBossPattern(BossPatternLogicType _logicType, List<BossBehaviourType> _behaviourTypeList)
    {
        BossPattern pattern = new BossPattern(model, bossTransform, _logicType);

        List<IBehaviour> behaviourList = new List<IBehaviour>();

        int count = _behaviourTypeList.Count;
        for (int i =0; i < count; i++)
        {
            IBehaviour behaviour = bossBehaviourArr[(int)_behaviourTypeList[i]];
            behaviourList.Add(behaviour);
        }

        pattern.SetBehaviour(behaviourList.ToArray());
        pattern.InitBehaviour();

        bossPatternList.Add(pattern);
    }
}
