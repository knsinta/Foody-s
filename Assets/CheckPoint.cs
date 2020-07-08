using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Sprite benderaKosong, benderaHijau;
    private SpriteRenderer checkpointSpriteRenderer;
    public bool sampecekpoin;
    // Start is called before the first frame update
    void Start()
    {
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            checkpointSpriteRenderer.sprite = benderaHijau;
            sampecekpoin = true;
        }
    }
}
