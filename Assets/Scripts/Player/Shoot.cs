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
    private const int numExplosive = 3;

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
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        numPress++;
                        if (numPress >= 5)
                        {
                            if (numPress >= 5 && health != null)
                            {
                                health.Damage(100);
                                patrol.GetComponent<Animator>().SetTrigger("Dead");
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
                                shootManager.AddScore(); 
                            }

                            if (text != null)
                            {
                                newText.text = " Zombies Killed: " + score.ToString();
                            }
                        }
                        EnemyAI enemyAI = FindObjectOfType<EnemyAI>();
                        enemyAI.isDead();
                        patrol.GetComponent<CharacterController>().enabled = false;
                    }
                }

                else if (hit.collider.tag == "E_Barrel")
                {
                    ExplosiveBarrel barrel = FindObjectOfType<ExplosiveBarrel>();
                    if(barrel != null)
                    {
                        barrel.ExplodeRed();
                    }
                    return; 
                }
                else if (hit.collider.tag == "F_Barrel")
                {
                    ExplosiveBarrel barrel = FindObjectOfType<ExplosiveBarrel>();
                    if(barrel != null)
                    {
                        barrel.ExplodeYellow();
                    }
                    return; 
                }
            }
        }
    }
}