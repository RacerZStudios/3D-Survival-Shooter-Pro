using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : MonoBehaviour
{
    [SerializeField]
    public GameObject r, c, l;

    [SerializeField]
    public GameObject l1, l2, l3;

    [SerializeField]
    public GameObject p1, p2, p3;

    public bool p1Restored, p2Restored, p3Restored; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && gameObject.name == "R")
        {
            r.GetComponent<MeshRenderer>().material.color = Color.clear;
            l1.SetActive(true);
            r.GetComponent<ParticleSystem>().Stop();
            p1.SetActive(true);
            p1Restored = true; 
        }
        if (other.gameObject.tag == "Player" && gameObject.name == "C")
        {
            c.GetComponent<MeshRenderer>().material.color = Color.clear;
            l2.SetActive(true);
            c.GetComponent<ParticleSystem>().Stop();
            p2.SetActive(true);
            p2Restored = true; 
        }
        if (other.gameObject.tag == "Player" && gameObject.name == "L")
        {
            l.GetComponent<MeshRenderer>().material.color = Color.clear;
            l3.SetActive(true);
            l.GetComponent<ParticleSystem>().Stop();
            p3.SetActive(true);
            p3Restored = true; 
        }
    }
}