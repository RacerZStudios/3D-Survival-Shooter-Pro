using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0; 
        }
        else if(Input.GetKeyDown(KeyCode.Tab))
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1; 
        }
    }
}
