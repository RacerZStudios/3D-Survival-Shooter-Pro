using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class LoadToMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0); 
    }
}
