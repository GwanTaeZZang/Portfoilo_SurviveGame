using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : Singleton<GlobalData>
{
    private int gold;

    public delegate void OnIncreaseGoldDelegate(int _gold);
    public OnIncreaseGoldDelegate OnIncreaseGoldEvnet;

    public override bool Initialize()
    {
        gold = 0;
        return base.Initialize();
    }

    public void InCreaseGold(int _amount)
    {
        gold += _amount;
        OnIncreaseGoldEvnet?.Invoke(gold);
    }

    public void DeCreaseGold(int _amount)
    {
        gold -= _amount;
        OnIncreaseGoldEvnet?.Invoke(gold);
    }

    public int GetGoldAmount()
    {
        return gold;
    }
}
