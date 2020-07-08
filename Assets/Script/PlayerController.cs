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
    
    private enum Status {diam, berlari, melompat, jatuh, sakit, mati}
    private Status status = Status.diam;

    [SerializeField]private AudioSource nabrak, dapet, lompat, super, cek;
    [SerializeField]private LayerMask ground;
    [SerializeField]private float speed = 3f;
    [SerializeField]private float jumpforce = 5f;
    [SerializeField]private int Pisang = 0;
    [SerializeField]private TextMeshProUGUI sehatText;
    [SerializeField]private float hurtforce = 5f;
    [SerializeField]private int health = 4;
    [SerializeField]private TextMeshProUGUI nyawaText;

    public int currentHealth;
    public int maxHealth = 4;
    public healthBar healthBar;
    public Vector3 responPoin;
    public bool tombolKiri, tombolKanan;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        nyawaText.text = health.ToString();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        responPoin = transform.position;
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
            dapet.Play();
            sehatText.text = Pisang.ToString();
        }

        if(sehat.tag == "PowerUp")
        {
            Destroy(sehat.gameObject);
            super.Play();
            jumpforce = 15f;
            speed = 10f;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            StartCoroutine(ResetPower());
        }
        
        if(sehat.tag == "Sensor Terjun")
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            transform.position = responPoin;
            PengaturNyawa();
        }

        if(sehat.tag == "Cek Poin")
        {
            cek.Play();
            responPoin = sehat.transform.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D tdksehat)
    {
        if(tdksehat.gameObject.tag == "Tidak Sehat")
        {
            Destroy(tdksehat.gameObject);
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
        } 
    }

    private void PengaturNyawa()
    {
        health -=1;
        currentHealth -= 1;
        healthBar.SetHealth(currentHealth);
        nyawaText.text = health.ToString();
        if(health <= 0)
        {
            
            status = Status.mati;
            // mati();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }       
    private void Movement()
    {
    float hDirection = Input.GetAxis("Horizontal");

        if(hDirection < 0 || (tombolKiri==true)) 
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }  

        else if(hDirection > 0 || (tombolKanan==true)) 
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
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
        lompat.Play();
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
        yield return new WaitForSeconds(7);
        jumpforce = 10;
        speed = 5;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    // private IEnumerator mati()
    // {
    //     yield return new WaitForSeconds(3);
    //     // status = Status.mati;
    //     // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    // }

    public void tekanKiri()
    {
        tombolKiri = true;
    }
    public void lepasKiri()
    {
        tombolKiri = false;
    }
    public void tekanKanan()
    {
        tombolKanan = true;
    }
    public void lepasKanan()
    {
        tombolKanan = false;
    }

    public void loncat()
    {
        if(coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }
}
