using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
        }
    }

    public void SaveCity()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1; 
    }
}
