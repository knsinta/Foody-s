using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
   public void BackMenu()
   {
       Destroy(GameObject.FindGameObjectWithTag("BGMusic"));
       SceneManager.LoadScene("MenuScene");     
   }
   public void Level1()
   {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("SampleScene");
   }

   public void Level2()
   {
        SceneManager.LoadScene("Level2Scene");
   }

   public void Level3()
   {
        SceneManager.LoadScene("Level3Scene");
   }

   public void Level4()
   {
        SceneManager.LoadScene("Level4Scene");
   }

   public void Level5()
   {
        SceneManager.LoadScene("Level5Scene");
   }
}
