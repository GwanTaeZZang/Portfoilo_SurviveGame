using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] JoyPad2DController joyPad;

    //private Vector2 moveDir;
    private float speed = 2;

    private void Start()
    {
        joyPad.Initialize(OnMoveCamera);
    }

    public void OnMoveCamera(Vector2 _dir)
    {
        if (_dir == Vector2.zero)
            return;
        //moveDir = _dir;

        Vector3 curPos = this.transform.position;
        curPos.x += _dir.x * speed * Time.deltaTime;
        curPos.y += _dir.y * speed * Time.deltaTime;
        this.transform.position = curPos;
    }
}
