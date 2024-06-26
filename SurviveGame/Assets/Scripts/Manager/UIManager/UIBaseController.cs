using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseController : MonoBehaviour
{
    [SerializeField] private Canvas myCanvas = null;
    private bool isShow = false;

    private void Awake()
    {
        if(myCanvas == null)
        {
            Debug.Log("myCanvas is null");
            myCanvas = this.GetComponent<Canvas>();
        }
    }

    public virtual void Show()
    {
        Debug.Log("Show Canvas");
        myCanvas.enabled = true;
        isShow = true;
    }

    public void Hide()
    {
        Debug.Log("Hide Canvas");
        myCanvas.enabled = false;
        isShow = false;
    }

    public void SetCanvasSortingOrder(int _amount)
    {
        myCanvas.sortingOrder = _amount;
    }

    public bool IsShow()
    {
        return isShow;
    }
}
