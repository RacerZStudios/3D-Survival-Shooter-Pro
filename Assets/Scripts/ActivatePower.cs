using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePower : MonoBehaviour
{
    public GameObject activatePowerR;
    [SerializeField]
    public PowerBox p1;
    [SerializeField]
    private CapsuleCollider p1Col;

    public GameObject activatePowerC;
    [SerializeField]
    public PowerBox p2;
    [SerializeField]
    private CapsuleCollider p2Col;

    public GameObject activatePowerL;
    [SerializeField]
    public PowerBox p3;
    [SerializeField]
    private CapsuleCollider p3Col;
    private void OnTriggerStay(Collider hit)
    {
        if(hit.gameObject.CompareTag("Player") && this.gameObject.tag == "SwitchR")
        {
            activatePowerR.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                p1.p1Restored = true;
            }
        }
        else if (hit.gameObject.CompareTag("Player") && this.gameObject.tag == "SwitchC")
        {
            activatePowerC.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                p2.p2Restored = true;
            }
        }
        else if (hit.gameObject.CompareTag("Player") && this.gameObject.tag == "SwitchL")
        {
            activatePowerL.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                p3.p3Restored = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (activatePowerR != null)
            {
                activatePowerR.gameObject.SetActive(false);
            }

            p1Col.enabled = false; 
            Destroy(activatePowerR.gameObject); 
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            if (activatePowerC != null)
            {
                activatePowerC.gameObject.SetActive(false);
            }

            p2Col.enabled = false;
            Destroy(activatePowerC.gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            if (activatePowerL != null)
            {
                activatePowerL.gameObject.SetActive(false);
            }

            p3Col.enabled = false;
            Destroy(activatePowerL.gameObject);
        }
    }
}
