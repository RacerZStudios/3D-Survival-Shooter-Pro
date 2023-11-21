using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSample : MonoBehaviour
{
    public AudioSource source;

    public bool isActive = false;
    public int i;

    public bool isMusicActive;

    private static MusicSample musicInstance;

    private void OnEnable()
    {
        if (source.isActiveAndEnabled)
        {
            source.Play();
            DontDestroyOnLoad(this);
        }
    }

    private void Awake()
    {
        if(musicInstance != null && musicInstance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            musicInstance = this; 
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isActive == false && source != null)
        {
            isActive = true;
            i += 1;
            if (i > 0)
            {
                source.Stop();
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.P) && isActive == true && source != null)
        {
            isActive = false;
            i = 0;
            if (i < 1)
            {
                source.Play();
            }
        }
    }
}