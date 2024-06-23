using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviour
{
    public void Initialize();
    public bool Update();
    public void SetBossMonsterModel(BossMonsterModel _model);
    public void SetTransform(Transform _bossTransform);
}
