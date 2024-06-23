using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossMoveBehaviour : IBehaviour
{
    protected BossMonsterModel model;
    protected Transform bossTransform;
    protected Transform playerTransform;

    protected Vector2 bossPos;
    protected Vector2 direction;
    protected float distance;

    public virtual void Initialize()
    {

    }

    public abstract void SetBossMonsterModel(BossMonsterModel _model);
    public abstract void SetTransform(Transform _bossTransform);
    public abstract bool Update();
}