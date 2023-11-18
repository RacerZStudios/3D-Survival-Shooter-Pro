using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
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

    [SerializeField]
    public Text text;
    [SerializeField]
    public Text newText;

    [SerializeField]
    public Shoot shootManager; 

    private int score = 0; 

    [SerializeField]
    public int count;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform bulletPos;

    public bool dead;

    public void AddScore()
    {
        score += 1;
       // text.text = score.ToString() + " Remaining Kills" + count; 
    }

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
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8 | 1 << 0)) // << bit shift operator and | or both layers masks 
            {
               // Debug.Log(hit.collider.name + "Hit");
                Health health = hit.collider.GetComponent<Health>();
                if (health != null)
                {
                    // blood splat effect 
                    // position of raycast hit 
                    // rotate toward hit normal (surface normal) 
                    Instantiate(bloodSplat, hit.point, Quaternion.LookRotation(hit.normal));
                   // Instantiate(bulletPrefab, hit.point, Quaternion.LookRotation(hit.normal)); 
                    health.Damage(5);
                    EnemyAI_Patrol patrol = hit.collider.GetComponent<EnemyAI_Patrol>();
                    patrol.isHit = true;
                    patrol.GetComponent<Animator>().SetTrigger("Hit");
                    // check how many times hit
                    // Debug.Log(5, health);
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        numPress++;
                        if (numPress >= 5)
                        {
                            //  Debug.Log("Reached 5");
                            if (numPress >= 5 && health != null)
                            {
                                health.Damage(100);
                                patrol.GetComponent<Animator>().SetTrigger("Dead");

                                // Destroy(GameObject.Find("Zombie_Rigged"), 3);
                            }
                        }
                    }

                   else if (patrol.isHit == true && health.isDead == true)
                    {
                        dead = true; 
                        if(dead == true)
                        {
                            if(count == 0 || count != 0)
                            {
                                count++;
                               // Debug.Log(count);
                                shootManager.AddScore(); 
                            }

                            if (text != null)
                            {
                                newText.text = " Zombies Killed: " + score.ToString();
                               // Debug.Log("AIDead");
                            }
                        }
                        EnemyAI enemyAI = FindObjectOfType<EnemyAI>();
                        enemyAI.isDead();
                        patrol.GetComponent<CharacterController>().enabled = false;
                    }
                }

                else if (hit.collider.tag == "E_Barrel")
                {
                    red = true;
                    // Debug.Log(hit);
                    ExplosiveBarrel g = FindObjectOfType<ExplosiveBarrel>();
                    // g.transform.position = hit.transform.position; 
                    for (int i = 0; i < numExplosive; i++)
                    {
                        if (red == true)
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
                        if (yellow == true)
                        {
                            f.ExplodeYellow();
                        }
                    }
                }
            }
        }
    }
}