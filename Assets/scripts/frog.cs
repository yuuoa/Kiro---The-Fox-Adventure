using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : Enemy
{

    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private LayerMask ground;

    private Collider2D coll;

    private bool facingLeft = true;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(anim.GetBool("Jump"))
        {
            if(rb.velocity.y < 0.1)
            {
                anim.SetBool("Fall", true);
                anim.SetBool("Jump", false);
                Movement();
            }
        }
        else if(coll.IsTouchingLayers(ground) && anim.GetBool("Fall"))
        {
            anim.SetBool("Fall", false);
        }
    }

    private void Movement()
    {
        if (facingLeft)
        {
            if(transform.position.x > leftCap)
            {
                if(transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1); 
                }
                if(coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jump", true);
                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            if(transform.position.x < rightCap)
            {
                if(transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1); 
                }
                if(coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jump", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }
}
