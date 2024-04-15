using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tmpPlayerStatus
{
    public int demage;
    public int attackSpeed;
    public int speed;
    public int hp;
    public int def;
    public int longRangeDemage;
    public int shortRangeDemage;

    public tmpPlayerStatus()
    {
        demage = 0;
        attackSpeed = 0;
        speed = 0;
        hp = 0;
        def = 0;
        longRangeDemage = 0;
        shortRangeDemage = 0;
    }
}

public class ShopCanvas : MonoBehaviour
{
    [SerializeField] private List<ShopItmeElement> itemElementList;
    [SerializeField] private Text demageAmount;
    [SerializeField] private Text attackSpeedAmount;
    [SerializeField] private Text speedAmount;
    [SerializeField] private Text hpAmount;
    [SerializeField] private Text defAmount;
    [SerializeField] private Text longRangeDemageAmount;
    [SerializeField] private Text shortRangeDemageAmount;

    private tmpPlayerStatus playerStatus;

    private void Awake()
    {
        itemElementList = new List<ShopItmeElement>();
        playerStatus = new tmpPlayerStatus();
    }
    private void Start()
    {

        UpdateStatusAmount(EffectType.Damage, 0);
        UpdateStatusAmount(EffectType.AtteckSpeed, 0);
        UpdateStatusAmount(EffectType.Speed, 0);
        UpdateStatusAmount(EffectType.Hp, 0);
        UpdateStatusAmount(EffectType.Def, 0);
        UpdateStatusAmount(EffectType.LongRangeDamege, 0);
        UpdateStatusAmount(EffectType.ShortRangeDamege, 0);


        BindButtonEvent();
    }

    private void BindButtonEvent()
    {
        int count = itemElementList.Count;
        for(int i =0; i < count; i++)
        {
            itemElementList[i].GetButtonEvent().AddListener(() => OnClickItemBtn(itemElementList[i].GetItemInfo()));
        }
    }
    private void OnClickItemBtn(ItemInfo _item)
    {
        List<Effect> buff = _item.buff;
        int buffCount = _item.buff.Count;
        for(int i =0; i < buffCount; i++)
        {
            UpdateStatusAmount(buff[i].effectType, buff[i].amount);
        }

        List<Effect> deBuff = _item.deBuff;
        int deBuffCount = _item.deBuff.Count;
        for(int i = 0; i < deBuffCount; i++)
        {
            UpdateStatusAmount(buff[i].effectType, buff[i].amount);
        }
    }
    private void UpdateStatusAmount(EffectType _type, int _amount)
    {
        if(_type == EffectType.Damage)
        {
            playerStatus.demage += _amount;
            demageAmount.text = playerStatus.demage.ToString();
        }
        if (_type == EffectType.AtteckSpeed)
        {
            playerStatus.attackSpeed += _amount;
            attackSpeedAmount.text = playerStatus.attackSpeed.ToString();

        }
        if (_type == EffectType.Speed)
        {
            playerStatus.speed += _amount;
            speedAmount.text = playerStatus.speed.ToString();

        }
        if (_type == EffectType.Hp)
        {
            playerStatus.hp += _amount;
            hpAmount.text = playerStatus.hp.ToString();

        }
        if (_type == EffectType.Def)
        {
            playerStatus.def += _amount;
            defAmount.text = playerStatus.def.ToString();

        }
        if (_type == EffectType.LongRangeDamege)
        {
            playerStatus.longRangeDemage += _amount;
            longRangeDemageAmount.text = playerStatus.longRangeDemage.ToString();

        }
        if (_type == EffectType.ShortRangeDamege)
        {
            playerStatus.shortRangeDemage += _amount;
            shortRangeDemageAmount.text = playerStatus.shortRangeDemage.ToString();

        }

    }
}
