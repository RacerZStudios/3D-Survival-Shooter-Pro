using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour
{
    [SerializeField]
    public GameObject map;

    public bool mapActive = false;
    public int i; 

    // Start is called before the first frame update
    void Start()
    {
        if(map != null)
        {
            map.gameObject.SetActive(false); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            i += 1; 
            if(i > 0 && i < 2)
            {
                mapActive = true;
                map.gameObject.SetActive(true);
                i += 1;
                return; 
            }      
        }
        if (Input.GetKeyDown(KeyCode.M) && mapActive == true)
        {
            if(i > 0 && i > 2)
            {
                i = 0; 
                mapActive = false;
                map.gameObject.SetActive(false);
            }
        }
    }
}
