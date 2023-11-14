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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, 45); 
    }

    public void ExplodeRed()
    {
        StartCoroutine(TimerRed());

        if (isRed == true)
        {
            if (explosionParticleRed == true)
            {
                GameObject g = barrel.gameObject;
                g.transform.parent = transform;

                StartCoroutine(ExplosionParticleRed()); 
                if (gameObject != null)
                {
                    Collider[] contacts = Physics.OverlapSphere(transform.position, 15);

                    foreach (Collider collider in contacts)
                    {
                        if (collider.CompareTag("Zombie"))
                        {
                            // play dead animation Zombie 
                            collider.gameObject.GetComponent<Animator>().SetTrigger("Dead"); 
                        }
                    }
                }
            }
            else if (gameObject == null)
            {
                Debug.LogError("Particle System Destroyed");
            }
        }
    }

    public void ExplodeYellow()
    {
        StartCoroutine(TimerYellow());

        if (isYellow == true)
        {
            if (explosionParticleYellow == true)
            {
                GameObject f = barrel.gameObject;
                f.transform.parent = transform; 

                StartCoroutine(ExplosionParticleYellow());
                if (gameObject != null)
                {
                    Collider[] contacts = Physics.OverlapSphere(transform.position, 25);

                    foreach (Collider collider in contacts)
                    {
                        if (collider.CompareTag("Zombie"))
                        {
                            // play dead animation Zombie 
                            collider.gameObject.GetComponent<Animator>().SetTrigger("Dead");
                        }
                    }
                }
            }
            else if (gameObject == null)
            {
                Debug.LogError("Particle System Destroyed");
            }
        }
    }

    private IEnumerator TimerRed()
    {
        isRed = true; 
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
                    yield return new WaitForSeconds(1f);
                }
            }        
        }
    }

    private IEnumerator TimerYellow()
    {
        isYellow = true; 
        fuseTime++;
        if (fuseTime >= 3)
        {
            StartCoroutine(FireParticleSpawnYellow());
            if (initparticleYellow == true && isYellow == true)
            {
                explosionParticleYellow = true;
                if (explosionParticleYellow == true)
                {
                    initparticleYellow = false;
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
        rb.AddExplosionForce(15, transform.position, 25, 0);
        explosionParticleYellow = false;
        yield return new WaitForEndOfFrame();
        Destroy(gameObject, 4f);      
    }
}