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

            UIManager.getInstance.Show<FirstCanvasController>("FirstCanvas");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UIManager.getInstance.Show<SecondCanvasController>("SecondCanvas");

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UIManager.getInstance.Show<ThirdCanvasController>("ThirdCanvas");

        }

        if (Input.GetKeyDown("d"))
        {
            UIManager.getInstance.Hide();
        }
    }
}
