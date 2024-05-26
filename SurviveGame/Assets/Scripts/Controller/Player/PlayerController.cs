using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerSpriteRenderer;
    [SerializeField] JoyPad2DController joyPad;
    //[SerializeField] private NavMeshAgent agent;

    private Character character;
    private float speed = 3;

    private void Awake()
    {
        joyPad.Initialize(OnMove);

        PlayerManager.getInstance.SetPlayer(this.transform);

        //agent.updateRotation = false;
        //agent.updateUpAxis = false;

    }
    public void Initialize()
    {
        character = new Character();
        //character.job = PlayerManager.getInstance.GetSelectedJob();
        character.job = PlayerManager.getInstance.GetJobList()[0];
        SetPlayerSprite(Resources.Load<Sprite>(character.job.jobSpritePath));

        CollisionManager.getInstance.SetPlayer(this.GetComponent<OBBCollision>());

        //speed = character.statusArr[(int)StatusEffectType.Speed].status;
    }

    private void OnMove(Vector2 _dir)
    {
        if (_dir == Vector2.zero)
            return;

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
