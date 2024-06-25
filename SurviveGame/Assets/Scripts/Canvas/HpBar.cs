using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] GameObject hpBar;
    [SerializeField] Image hpImage;

    public void ShowHpIamgeFillAmount(float _amount)
    {
        hpImage.fillAmount = _amount;
    }
}
