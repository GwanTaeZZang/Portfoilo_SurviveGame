using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float HALF = 0.5f;
    private const int CAMERA_VIEW_X_RANGE = 5;
    private const int CAMERA_VIEW_Y_RANGE = 12;

    [SerializeField] JoyPad2DController joyPad;
    private Character character;
    private TileMapModel mapData;
    private PlayerController player;
    private float cameraSpeed;

    private void Start()
    {
        joyPad.Initialize(OnMoveCamera);
        character = PlayerManager.getInstance.GetCharacter();

        player = PlayerManager.getInstance.GetPlayer();

    }

    public void Initialize(TileMapModel _mapData)
    {
        mapData = _mapData;
    }

    public void OnMoveCamera(Vector2 _dir)
    {
        if (_dir == Vector2.zero)
            return;

        //float speed = character.statusArr[(int)CharacterStatusType.P_Speed].status;
        cameraSpeed = character.statusArr[(int)CharacterStatusType.P_Speed].status;


        Vector2 playerPos = player.transform.position;

        Vector3 curPos = this.transform.position;

        if (playerPos.x < mapData.width * HALF - CAMERA_VIEW_X_RANGE &&
           playerPos.x > -(mapData.width * HALF - CAMERA_VIEW_X_RANGE))
        {
            curPos.x += _dir.x * cameraSpeed * Time.deltaTime;
            //curPos.y = this.transform.position.y;

            //curPos.x = this.transform.position.x;
            //curPos.y += _dir.y * cameraSpeed * Time.deltaTime;
        }

        if (playerPos.y < mapData.height * HALF - CAMERA_VIEW_Y_RANGE &&
           playerPos.y > -(mapData.height * HALF - CAMERA_VIEW_Y_RANGE))
        {
            //curPos.x += _dir.x * cameraSpeed * Time.deltaTime;
            //curPos.y = this.transform.position.y;

            //curPos.x = this.transform.position.x;
            curPos.y += _dir.y * cameraSpeed * Time.deltaTime;
        }

        this.transform.position = curPos;



        //if (curPos.x > mapData.width * HALF - CAMERA_VIEW_X_RANGE ||
        //    curPos.x < -(mapData.width * HALF - CAMERA_VIEW_X_RANGE))
        //{
        //    curPos.x = this.transform.position.x;
        //}
        //if (curPos.y > mapData.height * HALF - CAMERA_VIEW_Y_RANGE ||
        //    curPos.y < -(mapData.height * HALF - CAMERA_VIEW_Y_RANGE))
        //{
        //    curPos.y = this.transform.position.y;
        //}

    }
}
