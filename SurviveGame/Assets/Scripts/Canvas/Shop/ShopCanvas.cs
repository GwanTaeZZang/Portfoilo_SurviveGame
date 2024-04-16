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
    private List<Item> tmpItemList;

    private void Awake()
    {
        //itemElementList = new List<ShopItmeElement>();
        tmpItemList = new List<Item>();
        playerStatus = new tmpPlayerStatus();
    }
    private void Start()
    {
        LoadTmpItemData();

        UpdateStatusAmount(EffectType.Damage, 0);
        UpdateStatusAmount(EffectType.AtteckSpeed, 0);
        UpdateStatusAmount(EffectType.Speed, 0);
        UpdateStatusAmount(EffectType.Hp, 0);
        UpdateStatusAmount(EffectType.Def, 0);
        UpdateStatusAmount(EffectType.LongRangeDamege, 0);
        UpdateStatusAmount(EffectType.ShortRangeDamege, 0);

        UpdateItemElementInfo();
        BindButtonEvent();
    }

    private void BindButtonEvent()
    {
        int count = itemElementList.Count;
        for(int i =0; i < count; i++)
        {
            int index = i;
            itemElementList[i].GetButtonEvent().AddListener(() => OnClickItemBtn(itemElementList[index].GetItemInfo()));
        }
    }

    private void UpdateItemElementInfo()
    {
        int count = itemElementList.Count;
        for (int i = 0; i < count; i++)
        {
            itemElementList[i].UpdateItemInformation(tmpItemList[i]);
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
            UpdateStatusAmount(deBuff[i].effectType, deBuff[i].amount);
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

    private void LoadTmpItemData()
    {
        Item item = new Item();
        item.itemInfo = new ItemInfo();
        item.itemInfo.itemName = "aaaa";

        item.itemInfo.buff = new List<Effect>();

        Effect effect = new Effect();
        effect.stringKey = "a_buff";
        effect.effectType = EffectType.Damage;
        effect.amount = +3;
        item.itemInfo.buff.Add(effect);

        effect = new Effect();
        effect.stringKey = "b_buff";
        effect.effectType = EffectType.Speed;
        effect.amount = +2;
        item.itemInfo.buff.Add(effect);

        item.itemInfo.deBuff = new List<Effect>();

        effect = new Effect();
        effect.stringKey = "c_deBuff";
        effect.effectType = EffectType.AtteckSpeed;
        effect.amount = -3;
        item.itemInfo.deBuff.Add(effect);

        effect = new Effect();
        effect.stringKey = "d_deBuff";
        effect.effectType = EffectType.Hp;
        effect.amount = -2;
        item.itemInfo.deBuff.Add(effect);

        tmpItemList.Add(item);
        ///////////////////////////////////////////
        item = new Item();
        item.itemInfo = new ItemInfo();
        item.itemInfo.itemName = "bbbb";

        item.itemInfo.buff = new List<Effect>();

        effect = new Effect();
        effect.stringKey = "a_buff";
        effect.effectType = EffectType.Damage;
        effect.amount = +3;
        item.itemInfo.buff.Add(effect);

        effect = new Effect();
        effect.stringKey = "b_buff";
        effect.effectType = EffectType.Speed;
        effect.amount = +2;
        item.itemInfo.buff.Add(effect);

        item.itemInfo.deBuff = new List<Effect>();

        effect = new Effect();
        effect.stringKey = "c_deBuff";
        effect.effectType = EffectType.AtteckSpeed;
        effect.amount = -3;
        item.itemInfo.deBuff.Add(effect);

        effect = new Effect();
        effect.stringKey = "d_deBuff";
        effect.effectType = EffectType.Hp;
        effect.amount = -2;
        item.itemInfo.deBuff.Add(effect);

        tmpItemList.Add(item);
        ///////////////////////////////////////////
        item = new Item();
        item.itemInfo = new ItemInfo();
        item.itemInfo.itemName = "cccc";

        item.itemInfo.buff = new List<Effect>();

        effect = new Effect();
        effect.stringKey = "a_buff";
        effect.effectType = EffectType.Damage;
        effect.amount = +3;
        item.itemInfo.buff.Add(effect);

        effect = new Effect();
        effect.stringKey = "b_buff";
        effect.effectType = EffectType.Speed;
        effect.amount = +2;
        item.itemInfo.buff.Add(effect);

        item.itemInfo.deBuff = new List<Effect>();

        effect = new Effect();
        effect.stringKey = "c_deBuff";
        effect.effectType = EffectType.AtteckSpeed;
        effect.amount = -3;
        item.itemInfo.deBuff.Add(effect);

        effect = new Effect();
        effect.stringKey = "d_deBuff";
        effect.effectType = EffectType.Hp;
        effect.amount = -2;
        item.itemInfo.deBuff.Add(effect);

        tmpItemList.Add(item);
        ///////////////////////////////////////////
        item = new Item();
        item.itemInfo = new ItemInfo();
        item.itemInfo.itemName = "dddd";

        item.itemInfo.buff = new List<Effect>();

        effect = new Effect();
        effect.stringKey = "a_buff";
        effect.effectType = EffectType.Damage;
        effect.amount = +3;
        item.itemInfo.buff.Add(effect);

        effect = new Effect();
        effect.stringKey = "b_buff";
        effect.effectType = EffectType.Speed;
        effect.amount = +2;
        item.itemInfo.buff.Add(effect);

        item.itemInfo.deBuff = new List<Effect>();

        effect = new Effect();
        effect.stringKey = "c_deBuff";
        effect.effectType = EffectType.AtteckSpeed;
        effect.amount = -3;
        item.itemInfo.deBuff.Add(effect);

        effect = new Effect();
        effect.stringKey = "d_deBuff";
        effect.effectType = EffectType.Hp;
        effect.amount = -2;
        item.itemInfo.deBuff.Add(effect);

        tmpItemList.Add(item);

    }
}
