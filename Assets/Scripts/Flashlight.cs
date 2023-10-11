using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField]
    public GameObject flashLight;

    public bool isActive = false;
    public int i;

    [SerializeField]
    private ToolDiscovery toolToUse; 

    // Start is called before the first frame update
    void Start()
    {
        if (flashLight != null)
        {
            flashLight.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && toolToUse.canPickUp == true)
        {
            i += 1;
            if (i > 0 && i < 2)
            {
                isActive = true;
                flashLight.gameObject.SetActive(true);
                i += 1;
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.F) && isActive == true)
        {
            if (i > 0 && i > 2)
            {
                i = 0;
                isActive = false;
                flashLight.gameObject.SetActive(false);
            }
        }
    }
}
