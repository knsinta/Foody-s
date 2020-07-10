using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelComplete : MonoBehaviour
{
    // [SerializeField]private string namaScene;
    // public AudioSource complete;
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if(collision.gameObject.tag == "Player")
    //     {
    //         complete.Play();
    //         SceneManager.LoadScene(namaScene);
    //     }
    // }

    public GameObject LevelDialog;
    public Text LevelStatus;
    public TextMeshProUGUI Score;

    public static LevelComplete instance;

    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if(collision.gameObject.tag == "Player")
    //     {
    //         LevelComplete.instance.ShowLevelDialog();
    //     }
    // }
    public void ShowLevelDialog(string status, string scores)
    {
        GetComponent<StarController>().meraihBintang();
        LevelDialog.SetActive(true);
        Time.timeScale = 0f;
        LevelStatus.text = status;
        Score.text = scores;
    }

}
