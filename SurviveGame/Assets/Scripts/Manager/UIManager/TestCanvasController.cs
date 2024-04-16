using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCanvasController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("invoke resources.load");
            //GameObject obj = Resources.Load("FirstCanvas") as GameObject;
            //Instantiate(obj);

            UIManager.Instance.Show<FirstCanvasController>("FirstCanvas");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UIManager.Instance.Show<SecondCanvasController>("SecondCanvas");

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UIManager.Instance.Show<ThirdCanvasController>("ThirdCanvas");

        }

        if (Input.GetKeyDown("d"))
        {
            UIManager.Instance.Hide();
        }
    }
}
