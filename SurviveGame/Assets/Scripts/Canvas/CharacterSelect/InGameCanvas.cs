using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameCanvas : MonoBehaviour
{
    [SerializeField] Text waveTimeText;
    [SerializeField] Text goldText;

    public void Awake()
    {
        GlobalData.getInstance.OnIncreaseGoldEvnet = OnIncreaseGold;
    }

    private void OnIncreaseGold(int _gold)
    {
        goldText.text = _gold.ToString();
    }

    public void SetwaveTimeText(int _time)
    {
        waveTimeText.text = _time.ToString();
    }
}
