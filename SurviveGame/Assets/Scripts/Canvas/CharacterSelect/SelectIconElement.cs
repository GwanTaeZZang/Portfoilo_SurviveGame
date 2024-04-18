using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectIconElement : MonoBehaviour
{
    [SerializeField] private Button characterSeletBtn;
    [SerializeField] private Image characterThumbnail;

    public void SetElementThumbnail(Sprite _sprite)
    {
        characterThumbnail.sprite = _sprite;
    }
    public Button.ButtonClickedEvent GetElementSelectBtnEvent()
    {
        return characterSeletBtn.onClick;
    }
}
