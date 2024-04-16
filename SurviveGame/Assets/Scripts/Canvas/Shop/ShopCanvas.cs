using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tmpPlayerStatus
{
    public float[] status;

    public tmpPlayerStatus()
    {
        status = new float[(int)StatusEffectType.Lenght];

        int count = status.Length;
        for(int i =0; i < count; i++)
        {
            status[i] = 0;
        }
    }
}

public class ShopCanvas : MonoBehaviour
{
    [SerializeField] private List<ShopItmeElement> itemElementList;
    [SerializeField] private Text[] statusAmountArr;
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

        UpdateStatusAmount(StatusEffectType.Damage, 0);
        UpdateStatusAmount(StatusEffectType.AtteckSpeed, 0);
        UpdateStatusAmount(StatusEffectType.Speed, 0);
        UpdateStatusAmount(StatusEffectType.Hp, 0);
        UpdateStatusAmount(StatusEffectType.Def, 0);
        UpdateStatusAmount(StatusEffectType.LongRangeDamege, 0);
        UpdateStatusAmount(StatusEffectType.ShortRangeDamege, 0);

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
        List<StatusEffect> buff = _item.buff;
        int buffCount = _item.buff.Count;
        for(int i =0; i < buffCount; i++)
        {
            UpdateStatusAmount(buff[i].effectType, buff[i].amount);
        }

        List<StatusEffect> deBuff = _item.deBuff;
        int deBuffCount = _item.deBuff.Count;
        for(int i = 0; i < deBuffCount; i++)
        {
            UpdateStatusAmount(deBuff[i].effectType, deBuff[i].amount);
        }
    }

    private void UpdateStatusAmount(StatusEffectType _type, int _amount)
    {
        playerStatus.status[(int)_type] += _amount;
        statusAmountArr[(int)_type].text = playerStatus.status[(int)_type].ToString();
    }

    private void LoadTmpItemData()
    {
        Item item = new Item();
        item.itemInfo = new ItemInfo();
        item.itemInfo.itemName = "aaaa";

        item.itemInfo.buff = new List<StatusEffect>();

        StatusEffect effect = new StatusEffect();
        effect.stringKey = "a_buff";
        effect.effectType = StatusEffectType.Damage;
        effect.amount = +3;
        item.itemInfo.buff.Add(effect);

        effect = new StatusEffect();
        effect.stringKey = "b_buff";
        effect.effectType = StatusEffectType.Speed;
        effect.amount = +2;
        item.itemInfo.buff.Add(effect);

        item.itemInfo.deBuff = new List<StatusEffect>();

        effect = new StatusEffect();
        effect.stringKey = "c_deBuff";
        effect.effectType = StatusEffectType.AtteckSpeed;
        effect.amount = -7;
        item.itemInfo.deBuff.Add(effect);

        effect = new StatusEffect();
        effect.stringKey = "d_deBuff";
        effect.effectType = StatusEffectType.Hp;
        effect.amount = -1;
        item.itemInfo.deBuff.Add(effect);

        tmpItemList.Add(item);
        ///////////////////////////////////////////
        item = new Item();
        item.itemInfo = new ItemInfo();
        item.itemInfo.itemName = "bbbb";

        item.itemInfo.buff = new List<StatusEffect>();

        effect = new StatusEffect();
        effect.stringKey = "a_buff";
        effect.effectType = StatusEffectType.Damage;
        effect.amount = +5;
        item.itemInfo.buff.Add(effect);

        effect = new StatusEffect();
        effect.stringKey = "b_buff";
        effect.effectType = StatusEffectType.Speed;
        effect.amount = +3;
        item.itemInfo.buff.Add(effect);

        item.itemInfo.deBuff = new List<StatusEffect>();

        effect = new StatusEffect();
        effect.stringKey = "c_deBuff";
        effect.effectType = StatusEffectType.AtteckSpeed;
        effect.amount = -1;
        item.itemInfo.deBuff.Add(effect);

        effect = new StatusEffect();
        effect.stringKey = "d_deBuff";
        effect.effectType = StatusEffectType.Hp;
        effect.amount = -4;
        item.itemInfo.deBuff.Add(effect);

        tmpItemList.Add(item);
        ///////////////////////////////////////////
        item = new Item();
        item.itemInfo = new ItemInfo();
        item.itemInfo.itemName = "cccc";

        item.itemInfo.buff = new List<StatusEffect>();

        effect = new StatusEffect();
        effect.stringKey = "a_buff";
        effect.effectType = StatusEffectType.Damage;
        effect.amount = +100;
        item.itemInfo.buff.Add(effect);

        effect = new StatusEffect();
        effect.stringKey = "b_buff";
        effect.effectType = StatusEffectType.Speed;
        effect.amount = +2;
        item.itemInfo.buff.Add(effect);

        item.itemInfo.deBuff = new List<StatusEffect>();

        effect = new StatusEffect();
        effect.stringKey = "c_deBuff";
        effect.effectType = StatusEffectType.AtteckSpeed;
        effect.amount = -3;
        item.itemInfo.deBuff.Add(effect);

        effect = new StatusEffect();
        effect.stringKey = "d_deBuff";
        effect.effectType = StatusEffectType.Hp;
        effect.amount = -100;
        item.itemInfo.deBuff.Add(effect);

        tmpItemList.Add(item);
        ///////////////////////////////////////////
        item = new Item();
        item.itemInfo = new ItemInfo();
        item.itemInfo.itemName = "dddd";

        item.itemInfo.buff = new List<StatusEffect>();

        effect = new StatusEffect();
        effect.stringKey = "a_buff";
        effect.effectType = StatusEffectType.Damage;
        effect.amount = +3;
        item.itemInfo.buff.Add(effect);

        effect = new StatusEffect();
        effect.stringKey = "b_buff";
        effect.effectType = StatusEffectType.Speed;
        effect.amount = +2;
        item.itemInfo.buff.Add(effect);

        item.itemInfo.deBuff = new List<StatusEffect>();

        effect = new StatusEffect();
        effect.stringKey = "c_deBuff";
        effect.effectType = StatusEffectType.AtteckSpeed;
        effect.amount = -3;
        item.itemInfo.deBuff.Add(effect);

        effect = new StatusEffect();
        effect.stringKey = "d_deBuff";
        effect.effectType = StatusEffectType.Hp;
        effect.amount = -2;
        item.itemInfo.deBuff.Add(effect);

        tmpItemList.Add(item);

    }
}
