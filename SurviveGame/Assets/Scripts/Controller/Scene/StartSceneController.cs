using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour
{
    [SerializeField] private Button gameStartBtn;

    private void Start()
    {
        gameStartBtn.onClick.AddListener(OnClickGameStartBtn);
    }

    private void OnClickGameStartBtn()
    {
        UIManager.getInstance.Show<CharacterSelectCanvas>("Canvas/CharacterSelectCanvas");
    }
}
