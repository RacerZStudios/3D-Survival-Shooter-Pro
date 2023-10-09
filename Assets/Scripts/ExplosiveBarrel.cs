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

    private void Awake()
    {
        if(barrel != null)
        {
            barrel = gameObject;
            rb = GetComponent<Rigidbody>();
            particleSystems[0] = GetComponentInChildren<ParticleSystem>();
            particleSystems[0] = GameObject.Find("Init_Flame").GetComponent<ParticleSystem>();
            Debug.Log("P1 found"); 
            particleSystems[1] = GetComponentInChildren<ParticleSystem>();
            particleSystems[1] = GameObject.Find("Explosion").GetComponent<ParticleSystem>();
            Debug.Log("P2 found");
            if (particleSystems[1].gameObject.activeInHierarchy == true)
            {
                particleSystems[1].gameObject.SetActive(false);
                particleSystems[0].gameObject.SetActive(true);
            }
        }
    }

    public void Explode()
    {
        if(barrel == null)
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
            Collider[] contacts = Physics.OverlapSphere(transform.position, 15);

            foreach (Collider collider in contacts)
            {
                if (collider.CompareTag("Zombie") && explosionParticle == true)
                {
                    Destroy(collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8 | 1 << 0)) // << bit shift operator and | or both layers 
            {
                Debug.Log(hit.collider.name + "Hit");
                StartCoroutine(FireParticleSpawn());

                if(explosionParticle == true || initparticle == false && gameObject != null)
                {
                    particleSystems[1].gameObject.SetActive(true);
                    GameObject.Find("Explosion").gameObject.SetActive(true); 
                    StartCoroutine(ExplosionParticle());
                    rb.AddExplosionForce(5, centerPos, 5, 25);
                    Destroy(gameObject, 0.3f);
                }
                else if(gameObject == null)
                {
                    Debug.LogError("Particle System Destroyed"); 
                }
            }
        }
    }

    private IEnumerator FireParticleSpawn()
    {
        particleSystems[0].Play();
        initparticle = true; 
        yield return new WaitForSeconds(4f);
        Debug.Log("Add Force");
        initparticle = false;
        explosionParticle = true;
        Debug.Log("Explosion active");
    }

    private IEnumerator ExplosionParticle()
    {
        particleSystems[1].gameObject.GetComponent<GameObject>();
        GameObject.Find("Explosion").gameObject.SetActive(true);
        Debug.Log("Spawn Explosion Particle"); 
        particleSystems[1].Simulate(1, true, true); 
        particleSystems[1].Play();
        particleSystems[1].Emit(10);
        explosionParticle = false;
        yield return new WaitForEndOfFrame();
    }
}