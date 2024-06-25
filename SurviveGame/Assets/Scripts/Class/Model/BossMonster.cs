using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterModel
{
    public int Uid;
    public int hp;
    public float damage;
    public float speed;

    public BossPatternModel[] patternModelArr;

    public BossMonsterModel()
    {
        Uid = 9000;
        hp = 100;
        damage = 3f;
        speed = 2f;

        patternModelArr = new BossPatternModel[3];
        BossPatternModel patternModel = new BossPatternModel();
        patternModel.logicType = BossPatternLogicType.Loop;
        patternModel.behaviourTypeArr = new BossBehaviourType[2];
        patternModel.behaviourTypeArr[0] = BossBehaviourType.HexagonShoot;
        patternModel.behaviourTypeArr[1] = BossBehaviourType.BossApproachToTarget;
        patternModelArr[0] = patternModel;

        patternModel = new BossPatternModel();
        patternModel.logicType = BossPatternLogicType.Seqence;
        patternModel.behaviourTypeArr = new BossBehaviourType[2];
        patternModel.behaviourTypeArr[0] = BossBehaviourType.DashToTarget;
        patternModel.behaviourTypeArr[1] = BossBehaviourType.ContinuousHexagonShoot;
        patternModelArr[1] = patternModel;

        patternModel = new BossPatternModel();
        patternModel.logicType = BossPatternLogicType.Seqence;
        patternModel.behaviourTypeArr = new BossBehaviourType[2];
        patternModel.behaviourTypeArr[0] = BossBehaviourType.RampageShoot;
        patternModel.behaviourTypeArr[1] = BossBehaviourType.RandomDash;
        patternModelArr[2] = patternModel;

    }

}

public class BossPatternModel
{
    public BossPatternLogicType logicType;
    public BossBehaviourType[] behaviourTypeArr;
}

public enum BossPatternLogicType
{
    Seqence,
    Loop,
}

public enum BossBehaviourType
{
    DashToTarget = 0,
    BossApproachToTarget,
    RandomDash,
    BackRush,
    HexagonShoot,
    ContinuousHexagonShoot,
    TurningShoot,
    RampageShoot,
    End,
}
