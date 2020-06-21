using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    private void Update() 
    {
        if(Input.GetKey(KeyCode.A)) 
        {
            rb.velocity = new Vector2(-3, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            anim.SetBool("berlari", true);
        }  

        else if(Input.GetKey(KeyCode.D)) 
        {
            rb.velocity = new Vector2(3, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            anim.SetBool("berlari", true);
        }

        else
        {
            anim.SetBool("berlari", false);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }   
    }
}
