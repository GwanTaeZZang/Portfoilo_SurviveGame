using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttackBehaviour : IBehaviour
{
    protected BossMonsterModel model;
    protected Transform bossTransform;

    public virtual void Initialize()
    {

    }

    public abstract void SetBossMonsterModel(BossMonsterModel _model);
    public abstract void SetTransform(Transform _bossTransform);
    public abstract bool Update();
}