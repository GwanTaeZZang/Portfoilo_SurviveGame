using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour
{
    [SerializeField] private Button gameStartBtn;

    private void Awake()
    {
        //PlayerManager.getInstance.Initialize();
        //UIManager.getInstance.Initialize();
        //ObjectPoolManager.getInstance.Initialize();
        //ItemManager.getInstance.Initialize();
    }
    private void Start()
    {
        gameStartBtn.onClick.AddListener(OnClickGameStartBtn);
    }

    private void OnClickGameStartBtn()
    {
        UIManager.getInstance.Show<CharacterSelectCanvas>("Canvas/CharacterSelectCanvas");
    }
}
