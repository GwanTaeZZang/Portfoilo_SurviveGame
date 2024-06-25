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

    public BossPattern GetBossPattern(int _phase)
    {
        return bossPatternList[_phase];
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
        RampageShoot rampageShoot = new RampageShoot();

        bossBehaviourArr = new IBehaviour[(int)BossBehaviourType.End]
        {
            dashToTarget,
            bossApproachToTarget,
            randomDash,
            backRush,
            hexagonShoot,
            continuousHexagonShoot,
            turningShoot,
            rampageShoot
        };
    }

    public void CreateBossPattern(BossPatternModel[] _bossPatternModelArr)
    {
        int patternCount = _bossPatternModelArr.Length;
        for(int i =0; i < patternCount; i++)
        {
            BossPatternLogicType logicType = _bossPatternModelArr[i].logicType;
            BossBehaviourType[] behaviourTypeArr = _bossPatternModelArr[i].behaviourTypeArr;

            BossPattern pattern = new BossPattern(model, bossTransform, logicType);

            List<IBehaviour> behaviourList = new List<IBehaviour>();

            int count = behaviourTypeArr.Length;
            for (int j = 0; j < count; j++)
            {
                IBehaviour behaviour = bossBehaviourArr[(int)behaviourTypeArr[j]];
                behaviourList.Add(behaviour);
            }

            pattern.SetBehaviour(behaviourList.ToArray());
            pattern.InitBehaviour();

            bossPatternList.Add(pattern);
        }

    }
}
