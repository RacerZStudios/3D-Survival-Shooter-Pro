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

    public bool initparticleRed = false;
    public bool explosionParticleRed = false;
    public bool initparticleYellow = false;
    public bool explosionParticleYellow = false;

    [SerializeField]
    private ExplosiveBarrel[] explosiveBarrel;

    private MeshRenderer mat;
    public float fuseTime;
    public bool explode = false;
    public bool isRed, isYellow = false;

    private static int barrelCount = 6; 

    private void Awake()
    {
        if (barrel != null)
        {
            barrel = gameObject;
            mat = barrel.GetComponent<MeshRenderer>(); 
            rb = GetComponent<Rigidbody>();
            particleSystems[0] = GetComponentInChildren<ParticleSystem>();
            particleSystems[0] = GameObject.Find("Init_Flame").GetComponent<ParticleSystem>();
            particleSystems[1] = GetComponentInChildren<ParticleSystem>();
            particleSystems[1] = GameObject.Find("Explosion").GetComponent<ParticleSystem>();
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

    private void Start()
    {
        var objects = new GameObject[barrelCount];
        for(int i = 0; i < barrelCount; i++)
        {
            objects[i] = new GameObject();
        }

        var findObjects = FindObjectsOfType<GameObject>();
        findObjects.Length.ToString();
    }

    public void FindBarrelInstance()
    {
        Vector3 centerPos = new Vector3(0.5f, 0.5f, 0);
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(centerPos);

        Debug.DrawRay(ray.origin, Vector3.forward, Color.blue);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8 | 1 << 0)) // << bit shift operator and | or both layers ( 1 << 8 | 1 << 0) 
            {
                if (hit.collider.CompareTag("E_Barrel"))
                {
                    isRed = true;
                }
                else if (hit.collider.CompareTag("F_Barrel"))
                {
                    isYellow = true;
                }
            }        
        }
    }

    public void ExplodeRed()
    {
        isRed = true; 

        Vector3 centerPos = new Vector3(0.5f, 0.5f, 0);
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(centerPos);

        Debug.DrawRay(ray.origin, Vector3.forward, Color.blue);

        if(gameObject != null)
        {
            Collider[] contacts = Physics.OverlapSphere(transform.position, 25);

            foreach (Collider collider in contacts)
            {
                if (collider.CompareTag("Zombie"))
                {
                    Destroy(collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) 
            {
                if(hit.collider.gameObject.tag == "E_Barrel")
                {
                    Debug.Log(hit.collider.name + "Hit Red");
                    StartCoroutine(TimerRed()); 

                    if (explosionParticleRed == true)
                    {
                        particleSystems[1].gameObject.SetActive(true);
                        GameObject g = barrel.gameObject;
                        if (g != null)
                        {
                            g.transform.position = transform.position;
                        }
                    }
                    else if (gameObject == null)
                    {
                        Debug.LogError("Particle System Destroyed");
                    }
                }              
            }
        }
    }

    public void ExplodeYellow()
    {
        isYellow = true; 

        Vector3 centerPos = new Vector3(0.5f, 0.5f, 0);
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(centerPos);

        Debug.DrawRay(ray.origin, Vector3.forward, Color.blue);

        if (gameObject != null)
        {
            Collider[] contacts = Physics.OverlapSphere(transform.position, 55);

            foreach (Collider collider in contacts)
            {
                if (collider.CompareTag("Zombie"))
                {
                    Destroy(collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) 
            {
                if (hit.collider.gameObject.tag == "F_Barrel")
                {
                    Debug.Log(hit.collider.name + "Hit Yellow");
                    StartCoroutine(TimerYellow()); 
                    if (explosionParticleRed == true)
                    {
                        particleSystems[1].gameObject.SetActive(true);
                        GameObject g = barrel.gameObject;
                        if (g != null)
                        {
                            g.transform.position = transform.position;
                        }
                    }
                }
                else if (gameObject == null)
                {
                    Debug.LogError("Particle System Destroyed");
                }          
            }
        }
    }

    private IEnumerator TimerRed()
    {
        fuseTime++; 
        if(fuseTime >= 3)
        {
            StartCoroutine(FireParticleSpawn()); 
            if(initparticleRed == true && isRed == true)
            {
                explode = true;
                if (explosionParticleRed == true)
                {
                    initparticleRed = false; 
                    StartCoroutine(ExplosionParticleRed());
                    yield return new WaitForSeconds(1f);
                }
            }        
        }
    }

    private IEnumerator TimerYellow()
    {
        fuseTime++;
        if (fuseTime >= 3)
        {
            StartCoroutine(FireParticleSpawnYellow());
            if (initparticleYellow == true && isYellow == true)
            {
                explode = true;
                if (explosionParticleYellow == true)
                {
                    initparticleYellow = false;
                    StartCoroutine(ExplosionParticleYellow());
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }

    private IEnumerator FireParticleSpawn()
    {
        initparticleRed = true;
        particleSystems[0].Clear();
        particleSystems[0].Play();
        mat.material.color = Color.red;
        yield return new WaitForSeconds(4f);
        initparticleRed = false;
        explosionParticleRed = true;
        
    }

    private IEnumerator ExplosionParticleRed()
    {      
        explode = true;
        particleSystems[1].gameObject.SetActive(true);
        particleSystems[1].Simulate(1, true, true);
        particleSystems[1].Play();
        particleSystems[1].Emit(10);
        rb.AddExplosionForce(35, transform.position, 25, 35);
        explosionParticleRed = false;
        yield return new WaitForEndOfFrame();
        Destroy(gameObject, 2f);         
    }

    private IEnumerator FireParticleSpawnYellow()
    {
        initparticleYellow = true; 
        particleSystems[0].Clear();
        particleSystems[0].Play();
        mat.material.color = Color.blue;
        yield return new WaitForSeconds(6f);
        initparticleYellow = false;
        explosionParticleYellow = true;        
    }

    private IEnumerator ExplosionParticleYellow()
    {
        explode = true;
        particleSystems[1].gameObject.SetActive(true);
        particleSystems[1].Simulate(1, true, true);
        particleSystems[1].Play();
        particleSystems[1].Emit(10);
        rb.AddExplosionForce(55, transform.position, 55, 100);
        explosionParticleYellow = false;
        yield return new WaitForEndOfFrame();
        Destroy(gameObject, 4f);      
    }
}