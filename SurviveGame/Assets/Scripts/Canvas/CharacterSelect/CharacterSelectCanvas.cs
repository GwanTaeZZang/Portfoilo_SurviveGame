using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectCanvas : UIBaseController
{
    [SerializeField] private Button backBtn;
    [SerializeField] private Button selectCompleteBtn;
    [SerializeField] private Transform characterElementParent;
    [SerializeField] private SelectIconElement characterElementPrefab;
    [SerializeField] private Text selectCharacterName;
    [SerializeField] private Image selectCharacterImage;
    [SerializeField] private Text selectCharacterInfo;

    private List<SelectIconElement> characterElementList;
    private List<Job> jobList;
    private List<Sprite> characterSpriteList;
    private int curSelectedJobIdx;

    private void Start()
    {
        characterElementList = new List<SelectIconElement>();
        characterSpriteList = new List<Sprite>();
        jobList = PlayerManager.getInstance.GetJobList();
        backBtn.onClick.AddListener(OnClickBackBtn);
        selectCompleteBtn.onClick.AddListener(OnClickSelectCompleteBtn);
        SetCharacterElement();

        UpdateSelectCharacterInfo(0);
    }

    //public override void Show()
    //{
    //    base.Show();
    //    UpdateSelectCharacterInfo(0);
    //}

    private void SetCharacterElement()
    {
        int count = jobList.Count;
        for(int i = 0; i < count; i++)
        {
            int idx = i;
            SelectIconElement element = Instantiate(characterElementPrefab, characterElementParent);
            characterSpriteList.Add(Resources.Load<Sprite>(jobList[i].jobSpritePath));
            element.SetElementThumbnail(characterSpriteList[i]);
            element.GetElementSelectBtnEvent().AddListener(() => OnClickCharacterSelectBtn(idx));
            element.gameObject.SetActive(true);
            characterElementList.Add(element);
        }
    }

    private void OnClickCharacterSelectBtn(int _idx)
    {
        Debug.Log(_idx + "Click");
        UpdateSelectCharacterInfo(_idx);
    }

    private void UpdateSelectCharacterInfo(int _idx)
    {
        curSelectedJobIdx = _idx;
        Job job = jobList[_idx];
        selectCharacterName.text = job.jobName;
        selectCharacterImage.sprite = characterSpriteList[_idx];

        StringBuilder builder = new StringBuilder();
        builder.AppendLine("증가");
        int increaseCount = job.increaseStatus.Length;
        for(int i = 0; i < increaseCount; i++)
        {
            builder.AppendLine(job.increaseStatus[i].stringKey + "  " +job.increaseStatus[i].amount);
        }

        builder.AppendLine("감소");
        int decreaseCount = job.decreaseStatus.Length;
        for (int i = 0; i < decreaseCount; i++)
        {
            builder.AppendLine(job.decreaseStatus[i].stringKey + "  " + job.decreaseStatus[i].amount);
        }

        selectCharacterInfo.text = builder.ToString();
    }

    private void OnClickBackBtn()
    {
        this.Hide();
    }

    private void OnClickSelectCompleteBtn()
    {
        PlayerManager.getInstance.SetSelectedJob(jobList[curSelectedJobIdx]);
        UIManager.getInstance.Show<WeaponSelectCanvas>("Canvas/WeaponSelectCanvas");
    }
}
