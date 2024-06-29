using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearCanvas : UIBaseController
{
    [SerializeField] Button homeButton;

    private void Start()
    {
        homeButton.onClick.AddListener(AtHomeScene);
    }

    private void AtHomeScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
