using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolDiscovery : MonoBehaviour
{
    public bool canPickUp;
    [SerializeField]
    private GameObject pickUpText; 

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            pickUpText.SetActive(true); 
            if(Input.GetKeyDown(KeyCode.E))
            {
                canPickUp = true;
                Destroy(pickUpText);
                Destroy(gameObject);
            }
        }
    }
}
