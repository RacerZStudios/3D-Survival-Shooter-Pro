using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    public bool isActive = false; 
    public int i;

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
