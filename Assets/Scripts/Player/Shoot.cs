using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bloodSplat;
    //[SerializeField]
    //private LayerMask layerToHit; 

    // get full auto bool 
    [SerializeField]
    private Barreta_Pistol_Fire isFullAuto;

    [SerializeField]
    private int numPress;

    [SerializeField]
    public ExplosiveBarrel[] barrel;

    [SerializeField]
    private const int numExplosive = 3;

    public bool red, yellow, green; 

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        Vector3 centerPos = new Vector3(0.5f, 0.5f, 0);
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(centerPos);

        Debug.DrawRay(ray.origin, Vector3.forward, Color.blue);

        if (Input.GetMouseButtonDown(0) && isFullAuto.semiAuto == true || isFullAuto.fullAuto == true)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8 | 1 << 0)) // << bit shift operator and | or both layers 
            {
               // Debug.Log(hit.collider.name + "Hit");
                Health health = hit.collider.GetComponent<Health>();
                if (health != null)
                {
                    // blood splat effect 
                    // position of raycast hit 
                    // rotate toward hit normal (surface normal) 
                    Instantiate(bloodSplat, hit.point, Quaternion.LookRotation(hit.normal));
                    health.Damage(5);
                    // check how many times hit
                   // Debug.Log(5, health);
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        numPress++;
                        if (numPress >= 10)
                        {
                           //  Debug.Log("Reached 10");
                            if (numPress >= 10 && health != null)
                            {
                                health.Damage(100);
                                Destroy(GameObject.Find("Zombie_Rigged"));
                            }
                        }
                    }
                }

                else if (hit.collider.tag == "E_Barrel")
                {
                    red = true; 
                    // Debug.Log(hit);
                    ExplosiveBarrel g = FindObjectOfType<ExplosiveBarrel>();
                   // g.transform.position = hit.transform.position; 
                    for(int i = 0; i < numExplosive; i++)
                    {
                        if(red == true)
                        {
                            g.ExplodeRed();
                        }
                    }
                }
                else if (hit.collider.tag == "F_Barrel")
                {
                    yellow = true; 
                    // Debug.Log(hit);
                    ExplosiveBarrel f = FindObjectOfType<ExplosiveBarrel>();
                    // g.transform.position = hit.transform.position; 
                    for (int i = 0; i < numExplosive; i++)
                    {
                        if(yellow == true)
                        {
                            f.ExplodeYellow();
                        }
                    }
                }
            }
        }
    }
}