using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPlayerStatusView : MonoBehaviour
{
    [SerializeField] List<Text> statusAmountTextList;

    public void UpdateStatusAmount(CharacterStatus[] statusArr)
    {
        int count = statusArr.Length;

        for(int i =0; i < count; i++)
        {
            Text statusText = statusAmountTextList[i];
            float statusAmount = statusArr[i].status;

            statusText.text = statusAmount.ToString();
        }
    }
}
