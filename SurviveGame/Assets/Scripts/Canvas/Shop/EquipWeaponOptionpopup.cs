using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipWeaponOptionpopup : MonoBehaviour
{
    [SerializeField] private Button sellButton;
    [SerializeField] private Button synthesisButton;
    [SerializeField] private Button cancleButton;

    public Button.ButtonClickedEvent GetSellButtonEvent()
    {
        return sellButton.onClick;
    }

    public Button.ButtonClickedEvent GetSynthesisButtonEvent()
    {
        return synthesisButton.onClick;
    }

    public Button.ButtonClickedEvent GetCancleButtonEvent()
    {
        return cancleButton.onClick;
    }
}
