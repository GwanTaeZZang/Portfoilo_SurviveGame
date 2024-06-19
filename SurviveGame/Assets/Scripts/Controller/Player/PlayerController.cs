using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour , ITargetAble
{
    //private const float HALF = 0.5f;
    private const float COLLISION_DLEAY_TIME = 1f;

    [SerializeField] SpriteRenderer playerSpriteRenderer;
    [SerializeField] JoyPad2DController joyPad;
    [SerializeField] GameObject hpBar;
    [SerializeField] Image hpImage;

    //private ITargetAble[] targetArr;
    private BoxInfo playerBoxInfo;
    private Character character;
    private Camera mainCamera;

    private float curHP;

    private bool isCollision = true;
    private float invincibleTime = COLLISION_DLEAY_TIME;

    private void Awake()
    {
        mainCamera = Camera.main;

        joyPad.Initialize(OnMove);

        PlayerManager.getInstance.SetPlayer(this);

        playerBoxInfo = new BoxInfo();


    }
    public void Initialize()
    {
        character = PlayerManager.getInstance.GetCharacter();
        curHP = character.statusArr[(int)StatusEffectType.P_MaxHP].status;

        SetPlayerSprite(Resources.Load<Sprite>(character.job.jobSpritePath));

        playerBoxInfo.size = playerSpriteRenderer.bounds.size;

        hpBar.transform.position = mainCamera.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 0.55f));
    }

    private void Update()
    {
        if (!isCollision)
        {
            invincibleTime -= Time.deltaTime;
            if(invincibleTime < 0)
            {
                isCollision = true;
                invincibleTime = COLLISION_DLEAY_TIME;


                playerSpriteRenderer.color = Color.white;
            }
        }
    }

    private void OnMove(Vector2 _dir)
    {
        if (_dir == Vector2.zero)
            return;

        float speed = character.statusArr[(int)StatusEffectType.P_Speed].status;


        Vector3 curPos = this.transform.position;
        curPos.x += _dir.x * speed * Time.deltaTime;
        curPos.y += _dir.y * speed * Time.deltaTime;
        this.transform.position = curPos;
        playerBoxInfo.center = curPos;

        playerSpriteRenderer.flipX = _dir.x < 0;

        curPos.y += 1;
        hpBar.transform.position = mainCamera.WorldToScreenPoint(curPos);
    }

    private void SetPlayerSprite(Sprite _sprite)
    {
        playerSpriteRenderer.sprite = _sprite;
    }


    public bool IsCollision()
    {
        return isCollision;
    }

    public BoxInfo GetBoxInfo()
    {
        return playerBoxInfo;
    }

    public void OnDamege(int _damageAmount)
    {
        curHP -= _damageAmount;
        float fillAmount = curHP / character.statusArr[(int)StatusEffectType.P_MaxHP].status;
        hpImage.fillAmount = fillAmount;
        isCollision = false;

        playerSpriteRenderer.color = Color.red;

        Debug.Log("Ums");
    }
}
