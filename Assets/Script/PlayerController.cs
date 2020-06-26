using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    // private AudioSource suaraKaki;
    [SerializeField]private AudioSource nabrak, suaraKaki;

    
    // public int Lollipop = 0;
    
    private enum Status {diam, berlari, melompat, jatuh, sakit}
    private Status status = Status.diam;
    
    [SerializeField]private LayerMask ground;
    [SerializeField]private float speed = 3f;
    [SerializeField]private float jumpforce = 5f;
    [SerializeField]private int Pisang = 0;
    [SerializeField]private TextMeshProUGUI sehatText;
    [SerializeField]private float hurtforce = 5f;
    [SerializeField]private int health;
    [SerializeField]private TextMeshProUGUI nyawaText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        nyawaText.text = health.ToString();
        // suaraKaki = GetComponent<AudioSource>();
        // nabrak = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        if(status != Status.sakit)
        {
            Movement();
        }
        KecepatanStatus(); 
        anim.SetInteger("status", (int)status); 
    }
    
    private void OnTriggerEnter2D(Collider2D sehat)
    {
        if(sehat.tag == "Sehat")
        {
            Destroy(sehat.gameObject);
            Pisang += 1;
            sehatText.text = Pisang.ToString();
        }

        if(sehat.tag == "PowerUp")
        {
            Destroy(sehat.gameObject);
            jumpforce = 15f;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            StartCoroutine(ResetPower());
        }
    }
    private void OnCollisionEnter2D(Collision2D tdksehat)
    {
        if(tdksehat.gameObject.tag == "Tidak Sehat")
        {
            Destroy(tdksehat.gameObject);
            
            // Jump();
            status = Status.sakit;
            PengaturNyawa();
            nabrak.Play();
            if(tdksehat.gameObject.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(-hurtforce, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(hurtforce, rb.velocity.y);
            }
            // if(status == Status.jatuh)
            // {
                
            //     
            // }
        }

        // else
        // {
            
            
            // Lollipop -=1; 
    }

    private void PengaturNyawa()
    {
        health -=1;
        nyawaText.text = health.ToString();
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }       
    private void Movement()
    {
    float hDirection = Input.GetAxis("Horizontal");

        if(hDirection < 0) 
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            // suaraKaki.Play();
            // anim.SetBool("berlari", true);
        }  

        else if(hDirection > 0) 
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            // suaraKaki.Play();
            // anim.SetBool("berlari", true);
        }

        if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            status = Status.melompat;
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

        else if(status == Status.sakit)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
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

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(5);
        jumpforce = 10;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    // private void SuaraKaki()
    // {
    //     suaraKaki.Play();
    // }
}
