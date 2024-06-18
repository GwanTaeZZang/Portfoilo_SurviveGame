using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] JoyPad2DController joyPad;
    private Character character;

    private void Start()
    {
        joyPad.Initialize(OnMoveCamera);
        character = PlayerManager.getInstance.GetCharacter();
    }

    public void OnMoveCamera(Vector2 _dir)
    {
        if (_dir == Vector2.zero)
            return;

        float speed = character.statusArr[(int)StatusEffectType.P_Speed].status;


        Vector3 curPos = this.transform.position;
        curPos.x += _dir.x * speed * Time.deltaTime;
        curPos.y += _dir.y * speed * Time.deltaTime;
        this.transform.position = curPos;
    }
}
