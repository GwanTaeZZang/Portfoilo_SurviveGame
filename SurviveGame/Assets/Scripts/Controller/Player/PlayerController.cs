using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerSpriteRenderer;
    [SerializeField] JoyPad2DController joyPad;

    private Character character;
    //private Vector2 moveDir;
    private float speed = 2;

    private void Awake()
    {
        joyPad.Initialize(OnMove);
    }
    public void Initialize()
    {
        character = new Character();
        //character.job = PlayerManager.getInstance.GetSelectedJob();
        character.job = PlayerManager.getInstance.GetJobList()[0];
        SetPlayerSprite(Resources.Load<Sprite>(character.job.jobSpritePath));
    }

    private void OnMove(Vector2 _dir)
    {
        if (_dir == Vector2.zero)
            return;
        //moveDir = _dir;

        Vector3 curPos = this.transform.position;
        curPos.x += _dir.x * speed * Time.deltaTime;
        curPos.y += _dir.y * speed * Time.deltaTime;
        this.transform.position = curPos;

        playerSpriteRenderer.flipX = _dir.x < 0;
    }

    private void SetPlayerSprite(Sprite _sprite)
    {
        playerSpriteRenderer.sprite = _sprite;
    }

}
