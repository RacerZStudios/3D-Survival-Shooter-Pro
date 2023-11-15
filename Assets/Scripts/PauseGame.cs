using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    public int i;

    public bool pauseMenuActive = false;

    // Start is called before the first frame update
    void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            i += 1;
            if (i > 0 && i < 2)
            {
                pauseMenuActive = true;
                pauseMenu.gameObject.SetActive(true);
                i += 1;
                Time.timeScale = 0; 
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuActive == true)
        {
            if (i > 0 && i > 2)
            {
                i = 0;
                pauseMenuActive = false;
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1; 
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; 
    }
}