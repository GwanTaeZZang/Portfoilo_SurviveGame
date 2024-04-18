using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterElement : MonoBehaviour
{
    [SerializeField] private Button characterSeletBtn;
    [SerializeField] private Image characterThumbnail;

    public void SetCharacterThumbnail(Sprite _sprite)
    {
        characterThumbnail.sprite = _sprite;
    }
    public Button.ButtonClickedEvent GetCharacterSelectBtnEvent()
    {
        return characterSeletBtn.onClick;
    }
}
