using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infiniteBackground : MonoBehaviour
{

    private BoxCollider2D coll;
    private Rigidbody2D rb;

    private float width;
    private float speed = -1f;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        width = coll.size.x;
        rb.velocity = new Vector2 (speed, 0);
    }

    void Update()
    {
        if (transform.position.x < -width)
            Reposition();
    }

    private void Reposition()
    {
        Vector2 vector = new Vector2 (width * 2f, 0);
        transform.position = (Vector2)transform.position + vector;
    }
}
