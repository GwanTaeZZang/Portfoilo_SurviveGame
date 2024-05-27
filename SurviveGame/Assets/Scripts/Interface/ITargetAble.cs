using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetAble
{
    public bool IsCollision();
    public BoxInfo GetBoxInfo();
    public void OnDamege();
}
