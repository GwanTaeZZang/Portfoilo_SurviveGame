using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour , ITargetAble
{
    //private const float HALF = 0.5f;
    private const float COLLISION_DLEAY_TIME = 1f;
    private const int JOB_UID_INITIAL_VALUE = 1000;

    [SerializeField] SpriteRenderer playerSpriteRenderer;
    [SerializeField] JoyPad2DController joyPad;
    [SerializeField] HpBar hpBar;
    [SerializeField] CameraController cameraController;
    [SerializeField] List<RuntimeAnimatorController> animCrtlList;
    [SerializeField] Animator anim;

    //private ITargetAble[] targetArr;
    private BoxInfo playerBoxInfo;
    private Character character;
    private Camera mainCamera;
    private Vector2 inputVector;

    private float curHP;

    private bool isCollision = true;
    private bool isDead = false;
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
        curHP = character.statusArr[(int)CharacterStatusType.P_MaxHP].status;

        int jobUid = PlayerManager.getInstance.GetSelectedJob().Uid;
        anim.runtimeAnimatorController = animCrtlList[(jobUid % JOB_UID_INITIAL_VALUE)];
        SetPlayerSprite(Resources.Load<Sprite>(character.job.jobSpritePath));

        playerBoxInfo.size = playerSpriteRenderer.bounds.size;

        hpBar.transform.position = mainCamera.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 0.4f));
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

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        if(inputVector.magnitude > 0.01f)
        {
            //Debug.Log(inputVector.normalized.magnitude);
            OnMove(inputVector.normalized);
            cameraController.OnMoveCamera(inputVector.normalized);
        }
    }

    private void OnMove(Vector2 _dir)
    {
        //Debug.Log(_dir.magnitude);
        anim.SetFloat("Speed", _dir.magnitude);

        if (_dir == Vector2.zero)
            return;

        float speed = character.statusArr[(int)CharacterStatusType.P_Speed].status;


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

    public void ResetPlayer()
    {
        curHP = character.statusArr[(int)CharacterStatusType.P_MaxHP].status;
        hpBar.ShowHpIamgeFillAmount(1f);

        //this.transform.position = Vector2.zero;
        //Vector3 curPos = this.transform.position;
        //playerBoxInfo.center = curPos;
        //mainCamera.transform.position = new Vector3(0,0,-10);

        //curPos.y += 1;
        //hpBar.transform.position = mainCamera.WorldToScreenPoint(curPos);
    }

    public bool IsCollision()
    {
        return isCollision;
    }

    public BoxInfo GetBoxInfo()
    {
        return playerBoxInfo;
    }

    public void OnDamege(float _damageAmount)
    {
        curHP -= _damageAmount;
        float fillAmount = curHP / character.statusArr[(int)CharacterStatusType.P_MaxHP].status;
        hpBar.ShowHpIamgeFillAmount(fillAmount);
        isCollision = false;

        playerSpriteRenderer.color = Color.red;

        if(curHP <= 0)
        {
            isDead = true;
            anim.SetTrigger("Dead");
        }


        Debug.Log("Ums");
    }
}
