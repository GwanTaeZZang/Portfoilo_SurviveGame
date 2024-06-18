using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyPad2DController : MonoBehaviour
{
    private const float HALF = 0.5f;

    [SerializeField] private RectTransform joyStick;
    [SerializeField] private RectTransform touchArea;

    public delegate void JoyStickDelegate(Vector2 _dir);
    JoyStickDelegate OnJoyStickMove;

    private RectTransform joypadRect;
    private Vector2 oriJoyPadPos;
    private Vector2 zero = Vector2.zero;
    private Vector2 centerPos;
    private Vector2 dir;
    private float joyPadRadius;

    private bool isInit = false;
    private bool isMove;

    private void Start()
    {

        joypadRect = this.GetComponent<RectTransform>();
        oriJoyPadPos = joypadRect.position;
        joyPadRadius = joypadRect.sizeDelta.x * HALF;
        isMove = false;

        //SetTouchArea();
    }

    public void Initialize(JoyStickDelegate _delegate)
    {
        OnJoyStickMove += _delegate;
        isInit = true;
    }

    private void Update()
    {
        JoyPadUpdate();
    }

    public void JoyPadUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos = Input.mousePosition;
            InputJoyPadButtonDown(touchPos);
        }
        if (Input.GetMouseButtonUp(0))
        {
            joypadRect.position = oriJoyPadPos;
            joyStick.localPosition = zero;
            InputJoyPadButtonUp();
        }
        if (Input.GetMouseButton(0))
        {
            InputJoyPadButtonDrag(Input.mousePosition);
        }
        if (isInit)
        {
            OnJoyStickMove?.Invoke(dir.normalized);
        }

    }


    private void InputJoyPadButtonDown(Vector2 _mouesPos)
    {
        if (touchArea.position.x - touchArea.sizeDelta.x * HALF < _mouesPos.x &&
            touchArea.position.x + touchArea.sizeDelta.x * HALF > _mouesPos.x &&
            touchArea.position.y - touchArea.sizeDelta.y * HALF < _mouesPos.y &&
            touchArea.position.y + touchArea.sizeDelta.y * HALF > _mouesPos.y)
        {
            joypadRect.position = _mouesPos;
            joyStick.localPosition = zero;
            centerPos = _mouesPos;
            isMove = true;
        }
    }

    private void InputJoyPadButtonUp()
    {
        dir = Vector2.zero;
        isMove = false;
    }

    private void InputJoyPadButtonDrag(Vector2 _mouesPos)
    {
        if (isMove)
        {
            float distance = Vector2.Distance(_mouesPos, centerPos);
            dir = _mouesPos - centerPos;

            if (distance > joyPadRadius)
            {
                joyStick.position = centerPos + dir.normalized * joyPadRadius;
            }
            else
            {
                joyStick.position = centerPos + dir.normalized * distance;
            }
        }
    }

    private void SetTouchArea()
    {
        Vector2 originPivot = touchArea.pivot;
        Vector2 areaPos = touchArea.position;

        if (originPivot.x < HALF)
        {
            // 0 이라는 뜻 UI는 왼쪽
            touchArea.pivot = new Vector2(HALF, HALF);
            areaPos.x += touchArea.sizeDelta.x * HALF;
            areaPos.y += touchArea.sizeDelta.y * HALF;
            touchArea.position = areaPos;
        }
        if (originPivot.x > HALF)
        {
            // 1이라는 뜻 Ui는 오른쪽
            touchArea.pivot = new Vector2(HALF, HALF);
            areaPos.x -= touchArea.sizeDelta.x * HALF;
            areaPos.y += touchArea.sizeDelta.y * HALF;
            touchArea.position = areaPos;
        }
    }

}
