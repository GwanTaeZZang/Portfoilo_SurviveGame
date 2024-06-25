using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterModel
{
    public int Uid;
    public int hp;
    public float damage;
    public float speed;

    public BossPatternModel patternModel;

    public BossMonsterModel()
    {
        Uid = 9000;
        hp = 100;
        damage = 3f;
        speed = 3f;

        patternModel = new BossPatternModel();
        patternModel.logicType = BossPatternLogicType.Seqence;
        patternModel.behaviourTypeList = new List<BossBehaviourType>();
        patternModel.behaviourTypeList.Add(BossBehaviourType.HexagonShoot);
        patternModel.behaviourTypeList.Add(BossBehaviourType.RandomDash);
    }

}

public class BossPatternModel
{
    public BossPatternLogicType logicType;
    public List<BossBehaviourType> behaviourTypeList;
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
