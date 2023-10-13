using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class LoadGame : MonoBehaviour
{
   public void SaveCity()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1; 
    }
}
