using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectCanvas : UIBaseController
{
    [SerializeField] private Button backBtn;
    [SerializeField] private Button selectCompleteBtn;
    [SerializeField] private Button selectStageBtnPrefab;
    [SerializeField] private Transform selectStageBtnParent;
    [SerializeField] private Image selectedCharacterImage;
    [SerializeField] private Image selectedWeaponImage;
    [SerializeField] private Text selectedStageText;

    private int stageCount;
    private int curSelectedStageIdx;

    public void Start()
    {
        stageCount = StageManager.getInstance.GetStageCount();

        CreateStageBtn();
        backBtn.onClick.AddListener(OnClickBackBtn);
        selectStageBtnPrefab.onClick.AddListener(OnClickSelectCompleteBtn);
    }

    public override void Show()
    {
        selectedStageText.text = "Select Stage :  " + 0;
        SetSelectedImage();
        base.Show();
    }

    private void SetSelectedImage()
    {
        //int jobUid = PlayerManager.getInstance.GetSelectedJob().Uid;
        Job job = PlayerManager.getInstance.GetSelectedJob();
        selectedCharacterImage.sprite = Resources.Load<Sprite>(job.jobSpritePath);

        WeaponItemInfo weapon = ItemManager.getInstance.GetSelectedWeapon();
        selectedWeaponImage.sprite = Resources.Load<Sprite>(weapon.weaponSpritePath);
    }

    private void CreateStageBtn()
    {
        for(int i =0; i < stageCount; i++)
        {
            int idx = i;
            Button btn = Instantiate<Button>(selectStageBtnPrefab, selectStageBtnParent);
            Text text = btn.GetComponentInChildren<Text>();
            text.text = i.ToString();
            btn.gameObject.SetActive(true);
            btn.onClick.AddListener(() => OnClickSelectStageBtn(idx));
        }
    }

    private void OnClickBackBtn()
    {
        this.Hide();
    }

    private void OnClickSelectCompleteBtn()
    {
        StageManager.getInstance.SelectedStageIdx(curSelectedStageIdx);
        SceneManager.LoadScene("InGameScene");
    }

    private void OnClickSelectStageBtn(int _idx)
    {
        curSelectedStageIdx = _idx;
        selectedStageText.text = "Select Stage :  " + _idx;
    }
}
