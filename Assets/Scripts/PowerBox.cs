using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
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

    public GameObject textUpdate;
    [SerializeField]
    private Shoot shoot;

    public GameObject switchR;
    public GameObject switchC;
    public GameObject switchL;

    private void Start()
    {
        textUpdate.GetComponent<Text>().text = FindObjectOfType<Text>().text; 
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "Player" && gameObject.name == "R" && textUpdate.GetComponent<Text>().text.Length > 1 && shoot.count > 2)
        //{
        //    r.GetComponent<MeshRenderer>().material.color = Color.clear;
        //    l1.SetActive(true);
        //    r.GetComponent<ParticleSystem>().Stop();
        //    p1.SetActive(true);
        //    p1Restored = true; 
        //}
        //if (other.gameObject.tag == "Player" && gameObject.name == "C" && textUpdate.GetComponent<Text>().text.Length > 1 && shoot.count > 6)
        //{
        //    c.GetComponent<MeshRenderer>().material.color = Color.clear;
        //    l2.SetActive(true);
        //    c.GetComponent<ParticleSystem>().Stop();
        //    p2.SetActive(true);
        //    p2Restored = true; 
        //}
        //if (other.gameObject.tag == "Player" && gameObject.name == "L" && textUpdate.GetComponent<Text>().text.Length > 1 && shoot.count >= 12)
        //{
        //    l.GetComponent<MeshRenderer>().material.color = Color.clear;
        //    l3.SetActive(true);
        //    l.GetComponent<ParticleSystem>().Stop();
        //    p3.SetActive(true);
        //    p3Restored = true;
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && this.gameObject.name == "R") 
        {
            switchR.SetActive(true);
            if (p1Restored == true && gameObject.name == "R" && textUpdate.GetComponent<Text>().text.Length > 1 && shoot.count > 2)
            {
                r.GetComponent<MeshRenderer>().material.color = Color.clear;
                l1.SetActive(true);
                r.GetComponent<ParticleSystem>().Stop();
                p1.SetActive(true);
            }
        }
        if (other.gameObject.tag == "Player" && this.gameObject.name == "C")
        {
            switchC.SetActive(true);
            if (p2Restored == true && gameObject.name == "C" && textUpdate.GetComponent<Text>().text.Length > 1 && shoot.count > 6)
            {
                c.GetComponent<MeshRenderer>().material.color = Color.clear;
                l2.SetActive(true);
                c.GetComponent<ParticleSystem>().Stop();
                p2.SetActive(true);
            }
        }
        if (other.gameObject.tag == "Player" && this.gameObject.name == "L")
        {
            switchL.SetActive(true);
            if (p3Restored == true && gameObject.name == "L" && textUpdate.GetComponent<Text>().text.Length > 1 && shoot.count > 12)
            {
                l.GetComponent<MeshRenderer>().material.color = Color.clear;
                l3.SetActive(true);
                c.GetComponent<ParticleSystem>().Stop();
                p3.SetActive(true);
            }
        }
    }
}