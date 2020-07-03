using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class terjun : MonoBehaviour
{
    // public Rigidbody2D rb;
    // Vector3 posisiAwal;
    // public void UpdatePosisi(Vector3 Posisi) {
    //     posisiAwal = Posisi;
    // }
    // void Start()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    // }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // transform.position = posisiAwal;
            // rb.velocity = new Vector2(0f, 0f);
        }
    }
}
