using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController _instance;
    public static CameraController instance;

    public static GameObject player;

    Vector2 curMinBounds;
    Vector2 curMaxBounds;
    Vector3 targetPos;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        instance = _instance;
    }

    private void Start()
    {
        targetPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        SetTargetPos();
        transform.position = targetPos;
    }

    public void SetCamBounds(Vector2 minBounds, Vector2 maxBounds)
    {
        curMinBounds = minBounds;
        curMaxBounds = maxBounds;
    }

    private void SetTargetPos()
    {
        targetPos.x = player.transform.position.x;
        targetPos.y = player.transform.position.y;
        TestOutOfBounds();
    }

    private void TestOutOfBounds()
    {
        if (targetPos.x <= curMinBounds.x) { targetPos.x = curMinBounds.x; }
        if (targetPos.x >= curMaxBounds.x) { targetPos.x = curMaxBounds.x; }
        if (targetPos.y <= curMinBounds.y) { targetPos.y = curMinBounds.y; }
        if (targetPos.y >= curMaxBounds.y) { targetPos.y = curMaxBounds.y; }
    }
}
