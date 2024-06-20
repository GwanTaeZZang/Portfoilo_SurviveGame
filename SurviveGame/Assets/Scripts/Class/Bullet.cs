using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour , IPoolable
{
    [SerializeField] OBBCollision obbCollision;

    private int speed = 10;
    public Transform model;

    private Vector2 dir;
    private Vector2 startPos;

    private float damage;

    private void Start()
    {
        obbCollision.OnOBBCollisionEvent = OnOBBCollision;
    }

    private void Update()
    {
        if(dir != Vector2.zero)
        {
            Vector2 bulletPos = model.position;
            bulletPos.x += dir.x * Time.deltaTime * speed;
            bulletPos.y += dir.y * Time.deltaTime * speed;
            model.position = bulletPos;
        }
    }

    private void SetAngle()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        model.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    public void SetDirection(Vector2 _dir)
    {
        dir = _dir;

        SetAngle();
    }

    public void SetPosition(Vector2 _startPos)
    {
        startPos = _startPos;
    }

    public void SetTarget(params ITargetAble[] _target)
    {
        obbCollision.SetTarget(_target);
    }

    public void SetDamage(float _damageAmount)
    {
        damage = _damageAmount;
    }

    public void OnDequeue()
    {
        model.gameObject.SetActive(true);
        startPos.x += dir.x * 1f;
        startPos.y += dir.y * 1f;
        model.position = startPos;

        //obbCollision.enabled = true;
    }

    public void OnEnqueue()
    {
        model.gameObject.SetActive(false);
        dir = Vector2.zero;
    }

    public void SetModel(Transform _model)
    {
        model = _model;
    }

    public void SetSpeed(int _speed)
    {
        speed = _speed;
    }

    private void OnOBBCollision(ITargetAble _target)
    {
        _target.OnDamege(damage);
    }

}