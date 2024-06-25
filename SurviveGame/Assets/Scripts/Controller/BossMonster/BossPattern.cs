using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern
{
    private BossMonsterModel model;
    private Transform bossTransform;

    private IBehaviour[] bossBehaviourArr;
    private BossPatternLogicType logicType;
    private int seqenceCount = 0;
    private bool isPatternEnd = false;

    public BossPattern(BossMonsterModel _model, Transform _bossTransform, BossPatternLogicType _type)
    {
        model = _model;
        bossTransform = _bossTransform;
        logicType = _type;
    }

    public void SetBehaviour(params IBehaviour[] _behaviourArr)
    {
        bossBehaviourArr = _behaviourArr;
    }

    public void InitBehaviour()
    {
        int count = bossBehaviourArr.Length;
        for(int i =0; i < count; i++)
        {
            bossBehaviourArr[i].Initialize();
            bossBehaviourArr[i].SetBossMonsterModel(model);
            bossBehaviourArr[i].SetTransform(bossTransform);
        }
    }


    public bool UpdateBehaviour()
    {
        if(isPatternEnd == false)
        {
            if (logicType == BossPatternLogicType.Seqence)
            {
                UpdateSeqenceLogic();
            }
            else if (logicType == BossPatternLogicType.Loop)
            {
                UpdateLoopLogic();
            }
        }

        return isPatternEnd;
    }



    private void UpdateSeqenceLogic()
    {
        if(bossBehaviourArr.Length <= seqenceCount)
        {
            //isPatternEnd = true;
            InitBehaviour();
            seqenceCount = 0;
        }
        else
        {
            if (bossBehaviourArr[seqenceCount].Update())
            {
                seqenceCount++;
            }
        }
    }

    private void UpdateLoopLogic()
    {
        int count = bossBehaviourArr.Length;
        for(int i =0; i < count; i++)
        {
            if (bossBehaviourArr[i].Update())
            {
                bossBehaviourArr[i].Initialize();
            }
        }
    }
}
