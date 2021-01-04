using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public Animator anim;
    private Collider2D coll;
    public dialog dialogs;
    public dialogTrigger triggerDialog;
    private enum State {idle, moving, jumping, falling, hurt}
    private State state = State.idle;
    
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private int cherries  = 0;
    [SerializeField] private TextMeshProUGUI cherryText;
    [SerializeField] private float hurtforce = 5f;
    [SerializeField] private AudioSource jumping;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        jumping = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.deathOnJump();
                jump();
            }
            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2 (-hurtforce, 10);
                }
                else
                {
                    rb.velocity = new Vector2 (hurtforce, 10);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectible")
        {
            Destroy(collision.gameObject);
            cherries += 1;
            cherryText.text = cherries.ToString();
        }
        if(collision.tag == "dialog")
        {
            FindObjectOfType<dialogManager>().startDialog(dialogs);
        }
    }

    private void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }
        VelocitySwitch ();
        anim.SetInteger("State", (int)state);
    }
    
    private void Movement()
    {
        float hDirection = Input.GetAxis ("Horizontal");

        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            jump();
        }
    }

    private void jump()
    {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
            jumping.Play();
    }

    private void VelocitySwitch ()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.moving;
        }
        else if (!coll.IsTouchingLayers(ground) && state != State.jumping && state != State.falling)
        {
            state = State.falling;
        }
        else
        {
            state = State.idle;
        }
    }
}
