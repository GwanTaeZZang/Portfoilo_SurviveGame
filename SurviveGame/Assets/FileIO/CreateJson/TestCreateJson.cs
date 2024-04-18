using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCreateJson : MonoBehaviour
{
    private string jobFileName = "JobData";

    private JobData jobData;

    private void Awake()
    {
        CreateCharacterJobJson();
    }

    private void CreateCharacterJobJson()
    {
        Job[] jobArr = new Job[2];

        Job job1 = new Job();
        job1.Uid = 1000;
        job1.uniqueAbillity = null;

        job1.increaseStatus = new StatusEffect[1];

        StatusEffect effect = new StatusEffect();
        effect.stringKey = "������ ����";
        effect.effectType = StatusEffectType.Damage;
        effect.amount = +10;
        job1.increaseStatus[0] = effect;

        job1.decreaseStatus = new StatusEffect[2];

        effect = new StatusEffect();
        effect.stringKey = "���ݼӵ� ����";
        effect.effectType = StatusEffectType.AttackSpeed;
        effect.amount = -3;
        job1.decreaseStatus[0] = effect;

        effect = new StatusEffect();
        effect.stringKey = "�ӵ� ����";
        effect.effectType = StatusEffectType.Speed;
        effect.amount = -3;
        job1.decreaseStatus[1] = effect;

        job1.unLockId = 10003;

        job1.stringKey = "�� �ٺ�";

        job1.jobSpritePath = "Sprite/Job/Job_0";
        job1.jobName = "�� �ٺ�";

        jobArr[0] = job1;

        Job job2 = new Job();
        job2.Uid = 1001;
        job2.uniqueAbillity = null;

        job2.increaseStatus = new StatusEffect[2];

        effect = new StatusEffect();
        effect.stringKey = "���ݼӵ� ����";
        effect.effectType = StatusEffectType.AttackSpeed;
        effect.amount = +5;
        job2.increaseStatus[0] = effect;

        effect = new StatusEffect();
        effect.stringKey = "�ӵ� ����";
        effect.effectType = StatusEffectType.Speed;
        effect.amount = +5;
        job2.increaseStatus[1] = effect;

        job2.decreaseStatus = new StatusEffect[1];

        effect = new StatusEffect();
        effect.stringKey = "������ ����";
        effect.effectType = StatusEffectType.Damage;
        effect.amount = -7;
        job2.decreaseStatus[0] = effect;

        job2.unLockId = 10004;

        job2.stringKey = "�ӵ� �ٺ�";

        job2.jobSpritePath = "Sprite/Job/Job_1";
        job2.jobName = "�ӵ� �ٺ�";

        jobArr[1] = job2;


        jobData = new JobData();
        jobData.job = jobArr;
    }

    [ContextMenu("To Json Data")]  // ������Ʈ �޴��� �Ʒ� �Լ��� ȣ���ϴ� To Json Data ��� ��� ������ 
    private void SaveToJsonMapData()
    {
        if (jobFileName == "")
        {
            Debug.Log("���� �̸� ���� ");
            return;
        }
        JsonController.WriteJson<JobData>(jobFileName, jobData);
    }


}
