using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour , IPoolable
{
    public Transform model;

    private Vector2 dir;
    private Vector2 startPos;

    private void Update()
    {
        if(dir != Vector2.zero)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            model.rotation = Quaternion.Euler(0, 0, angle - 90f);

            Vector2 bulletPos = model.position;
            bulletPos.x += dir.x * Time.deltaTime * 10;
            bulletPos.y += dir.y * Time.deltaTime * 10;
            model.position = bulletPos;

        }
    }

    public void SetDirection(Vector2 _dir)
    {
        dir = _dir;
    }

    public void SetPosition(Vector2 _startPos)
    {
        startPos = _startPos;
    }

    public void OnDequeue()
    {
        model.gameObject.SetActive(true);
        startPos.x += dir.x * 1f;
        startPos.y += dir.y * 1f;
        model.position = startPos;
    }

    public void OnEnqueue()
    {
        model.gameObject.SetActive(false);
    }

    public void SetModel(Transform _model)
    {
        model = _model;
    }
}