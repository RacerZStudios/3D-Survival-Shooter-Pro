using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Streak : MonoBehaviour
{
    private static Streak instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this; 
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(transform.root.gameObject);
    }
}
