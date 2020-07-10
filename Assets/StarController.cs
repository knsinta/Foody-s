using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public GameObject[] stars;
    private int jumlahSehat;
    // public Sprite bintangKosong, bintangKuning;
    // private SpriteRenderer starSpriteRenderer;
    // public bool raihBintang;
    // Start is called before the first frame update
    void Start()
    {
        // starSpriteRenderer = GetComponent<SpriteRenderer>();
        jumlahSehat = GameObject.FindGameObjectsWithTag("Sehat").Length;
    }

    public void meraihBintang()
    {
        int sisaSehat = GameObject.FindGameObjectsWithTag("Sehat").Length;
        int ambilSehat = jumlahSehat - sisaSehat;

        float persentase = float.Parse(ambilSehat.ToString()) / float.Parse(jumlahSehat.ToString()) * 100f;

        if(persentase < 33)
        {
            stars[0].SetActive(false);
            stars[1].SetActive(false);
            stars[2].SetActive(false);
        }
        else if(persentase >= 33f && persentase < 66)
        {
            //1 bintang
            stars[0].SetActive(true);
        }
        else if(persentase >= 66 && persentase < 70)
        {
            //2 bintang
            stars[0].SetActive(true);
            stars[1].SetActive(true);
        }
        else
        {
            //3 bintang
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
