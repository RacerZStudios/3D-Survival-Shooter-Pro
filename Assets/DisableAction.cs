using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAction : MonoBehaviour
{
    [SerializeField]
    private GameObject t; 
    public void Disable()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.M))
        {
            t.SetActive(false);
        }
    }

    private void Update()
    {
        Disable(); 
    }
}
