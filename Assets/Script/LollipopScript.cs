using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LollipopScript : Musuh
{
    [SerializeField]private float kiri;
    [SerializeField]private float kanan;
    [SerializeField]private float panjangGerak = 10f;
    [SerializeField]private float tinggiGerak = 15f;
    [SerializeField]private LayerMask ground;

    private Collider2D coll;
    private Rigidbody2D rb;
    private bool balikKiri = true;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    private void Update()
    {
        if(balikKiri)
        {
            if(transform.position.x > kiri)
            {
                if(transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                if(coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-panjangGerak, tinggiGerak);
                }
            }
            else
            {
                balikKiri = false;
            }
        }

        else
        {
            if(transform.position.x < kanan)
            {
                if(transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                if(coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(panjangGerak, tinggiGerak);
                }
            }
            else
            {
                balikKiri = true;
            }
        }
    }
}
