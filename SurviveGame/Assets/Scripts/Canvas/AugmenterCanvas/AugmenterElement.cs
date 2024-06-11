using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AugmenterElement : MonoBehaviour
{
    [SerializeField] private Button augmenterBtn;
    [SerializeField] private Image augmenterIcon;
    [SerializeField] private Text augmenterTitle;
    [SerializeField] private Text augmenterInfo;


    public Button.ButtonClickedEvent GetAugmenterBtnEvent()
    {
        return augmenterBtn.onClick;
    }

    public void SetAugmenterIcon(Sprite _sprite)
    {
        augmenterIcon.sprite = _sprite;
    }

    public void SetAugmenterTitle(string _title)
    {
        augmenterTitle.text = _title;
    }

    public void SetAugmenterInfo(string _info)
    {
        augmenterInfo.text = _info;
    }
}
