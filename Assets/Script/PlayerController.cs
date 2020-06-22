using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;

    public int Pisang = 0;
    public int Lollipop = 0;
    
    private enum Status {diam, berlari, melompat, jatuh}
    private Status status = Status.diam;
    
    [SerializeField]private LayerMask ground;
    [SerializeField]private float speed = 3f;
    [SerializeField]private float jumpforce = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    private void Update() 
    {
        Movement(); 
        KecepatanStatus(); 
        anim.SetInteger("status", (int)status); 
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Sehat")
        {
            Destroy(collision.gameObject);
            Pisang += 1;
        }

        if(collision.tag == "Tidak Sehat")
        {
            Destroy(collision.gameObject);
            Lollipop -=1;
        }
    }
    private void Movement()
    {
    float hDirection = Input.GetAxis("Horizontal");

        if(hDirection < 0) 
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            // anim.SetBool("berlari", true);
        }  

        else if(hDirection > 0) 
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            // anim.SetBool("berlari", true);
        }

        if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            status = Status.melompat;
        }
    }
    private void KecepatanStatus()
    {
        if(status == Status.melompat)
        {
            if(rb.velocity.y < .1f)
            {
                status = Status.jatuh;
            }
        }

        else if(status == Status.jatuh)
        {
            if(coll.IsTouchingLayers(ground))
            {
                status = Status.diam;
            }
        }

        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            status = Status.berlari;
        }

        else
        {
            status = Status.diam;
        }
    }
}
