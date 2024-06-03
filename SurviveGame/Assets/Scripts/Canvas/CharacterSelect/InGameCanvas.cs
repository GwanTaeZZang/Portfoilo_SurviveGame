using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameCanvas : MonoBehaviour
{
    [SerializeField] Text waveTimeText;

    public void SetwaveTimeText(int _time)
    {
        waveTimeText.text = _time.ToString();
    }
}
