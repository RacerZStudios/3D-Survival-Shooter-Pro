using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolDiscovery : MonoBehaviour
{
    public bool canPickUp;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                canPickUp = true;
            }
        }
    }
}
