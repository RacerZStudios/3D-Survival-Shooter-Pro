using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField]
    public GameObject barrel;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    public ParticleSystem[] particleSystems = new ParticleSystem[2];

    public bool initparticle = false;
    public bool explosionParticle = false;

    [SerializeField]
    private ExplosiveBarrel[] explosiveBarrel;

    private MeshRenderer mat;
    public float fuseTime;
    public bool explode = false;
    public bool isRed, isYellow = false; 

    private void Awake()
    {
        if(barrel != null)
        {
            barrel = gameObject;
            mat = barrel.GetComponent<MeshRenderer>(); 
            rb = GetComponent<Rigidbody>();
            particleSystems[0] = GetComponentInChildren<ParticleSystem>();
            particleSystems[0] = GameObject.Find("Init_Flame").GetComponent<ParticleSystem>();
           // Debug.Log("P1 found"); 
            particleSystems[1] = GetComponentInChildren<ParticleSystem>();
            particleSystems[1] = GameObject.Find("Explosion").GetComponent<ParticleSystem>();
          // Debug.Log("P2 found");
            if (particleSystems[1].gameObject.activeInHierarchy == true)
            {
                particleSystems[1].gameObject.SetActive(false);
                particleSystems[0].gameObject.SetActive(true);
            }
        }

        foreach(ExplosiveBarrel explosiveBarrel in explosiveBarrel)
        {
            if(barrel != null)
            {
                explosiveBarrel.explosiveBarrel.Clone().Equals(barrel);
            }
        }
    }

    public void FindBarrelInstance()
    {
        if (gameObject.tag == "F_Barrel")
        {
            isYellow = true;
        }
        else if (gameObject.tag == "E_Barrel")
        {
            isRed = true;
        }
    }

    public void ExplodeRed()
    {
        if(barrel != null)
        {
            barrel = GetComponent<GameObject>();
            return; 
        }

        Vector3 centerPos = new Vector3(0.5f, 0.5f, 0);
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(centerPos);

        Debug.DrawRay(ray.origin, Vector3.forward, Color.blue);

        if(gameObject != null)
        {
            Collider[] contacts = Physics.OverlapSphere(transform.position, 25);

            foreach (Collider collider in contacts)
            {
                if (collider.CompareTag("Zombie") && explode == true)
                {
                    Destroy(collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && isRed == true)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8 | 1 << 0)) // << bit shift operator and | or both layers 
            {
               // Debug.Log(hit.collider.name + "Hit");
                StartCoroutine(FireParticleSpawn());

                if(explosionParticle == true || initparticle == false && gameObject != null)
                {
                    particleSystems[1].gameObject.SetActive(true);
                    GameObject g = this.gameObject;
                    if(g != null)
                    {
                        g.transform.position = transform.position; 
                    }                  
                }
                else if(gameObject == null)
                {
                    Debug.LogError("Particle System Destroyed"); 
                }
            }
        }
    }

    public void ExplodeYellow()
    {
        if (barrel != null)
        {
            barrel = GetComponent<GameObject>();
            ExplosionParticleYellow(); 
            return;
        }

        Vector3 centerPos = new Vector3(0.5f, 0.5f, 0);
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(centerPos);

        Debug.DrawRay(ray.origin, Vector3.forward, Color.blue);

        if (gameObject != null)
        {
            Collider[] contacts = Physics.OverlapSphere(transform.position, 55);

            foreach (Collider collider in contacts)
            {
                if (collider.CompareTag("Zombie") && explode == true)
                {
                    Destroy(collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && isYellow == true)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8 | 1 << 0)) // << bit shift operator and | or both layers 
            {
                if(hit.collider.tag == "F_Barrel" && hit.collider.tag != "E_Barrel")
                {
                    if (explosionParticle == true || initparticle == false && gameObject != null)
                    {
                        // Debug.Log(hit.collider.name + "Hit Yellow");
                        StartCoroutine(FireParticleSpawnYellow());
                      //   StartCoroutine(ExplosionParticleYellow()); 
                        particleSystems[1].gameObject.SetActive(true);
                        GameObject f = this.gameObject;
                        if (f != null)
                        {
                            f.transform.position = transform.position;
                        }
                        else if (gameObject == null)
                        {
                            Debug.LogError("Particle System Destroyed");
                        }
                    }
                }             
            }
        }
    }

    private void LateUpdate()
    {
        FindBarrelInstance(); 
        while(explosionParticle == true)
        {
            fuseTime++;
            StartCoroutine(Timer());
            break; 
        }
    }

    private IEnumerator Timer()
    {
        if(fuseTime >= 3)
        {
            explode = true;
            if (explosionParticle == true)
            {
                StartCoroutine(ExplosionParticle());
                yield return new WaitForSeconds(1f);
                Destroy(gameObject, 3); 
            }
        }
    }

    private IEnumerator FireParticleSpawn()
    {
        particleSystems[0].Play();
        initparticle = true;
        mat.material.color = Color.red; 
        yield return new WaitForSeconds(4f);
      //  Debug.Log("Add Force");
        initparticle = false;
        explosionParticle = true;
       // Debug.Log("Explosion active");
    }

    private IEnumerator ExplosionParticle()
    {
        explode = true; 
        particleSystems[1].gameObject.SetActive(true); 
       // Debug.Log("Spawn Explosion Particle");
        particleSystems[1].Simulate(1, true, true);
        particleSystems[1].Play();
        particleSystems[1].Emit(10);
        rb.AddExplosionForce(35, transform.position, 25, 35);
        //   Debug.Log("Exploded");
        explosionParticle = false;
        yield return new WaitForEndOfFrame();
        Destroy(gameObject, 0.3f);
    }

    private IEnumerator FireParticleSpawnYellow()
    {
        particleSystems[0].Play();
        initparticle = true;
        mat.material.color = Color.yellow;
        yield return new WaitForSeconds(6f);
        //  Debug.Log("Add Force");
        initparticle = false;
        explosionParticle = true;
        // Debug.Log("Explosion active");
    }

    private IEnumerator ExplosionParticleYellow()
    {
        explode = true;
        particleSystems[1].gameObject.SetActive(true);
        // Debug.Log("Spawn Explosion Particle");
        particleSystems[1].Simulate(1, true, true);
        particleSystems[1].Play();
        particleSystems[1].Emit(10);
        rb.AddExplosionForce(55, transform.position, 55, 100);
        //   Debug.Log("Exploded");
        explosionParticle = false;
        yield return new WaitForEndOfFrame();
        Destroy(gameObject, 0.3f);
    }
}