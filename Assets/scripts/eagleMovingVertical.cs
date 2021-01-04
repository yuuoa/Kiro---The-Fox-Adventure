using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagleMovingVertical : Enemy
{

    public Vector3 pos1;
    public Vector3 pos2;
    public float speed = 1f;

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time*speed, 1f));
    }
}
