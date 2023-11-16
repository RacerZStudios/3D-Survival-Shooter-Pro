using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField]
    public Transform lazerPos;
    [SerializeField]
    public ParticleSystem particleSystem;

    [SerializeField]
    public Light lazerLight;
    [SerializeField]
    public GameObject halo;

    public bool isActive = false;
    public int i;

    [SerializeField]
    public Transform targetRetPos;

    public LineRenderer lR;

    [SerializeField]
    private GameObject lazer;


    // Start is called before the first frame update
    void Start()
    {
        if (halo != null)
        {
            halo.gameObject.SetActive(false);
        }

        if (lazer != null)
        {
            lazer.gameObject.SetActive(false);
        }
    }

    void UpdateLazer()
    {
        if(isActive == true)
        {
            Vector3 startPos = lR.GetPosition(0);
            Vector3 endPos = lR.GetPosition(lR.positionCount - 1);

            lR.SetPosition(1, lR.transform.position);
            lR.SetPosition(0, lazerPos.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            i += 1;
            if (i > 0 && i < 2)
            {
                isActive = true;
                halo.gameObject.SetActive(true);
                lazer.gameObject.SetActive(true);
                UpdateLazer();
                i += 1;
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.T) && isActive == true)
        {
            if (i > 0 && i > 2)
            {
                i = 0;
                isActive = false;
                halo.gameObject.SetActive(false);
                lazer.gameObject.SetActive(false);
            }
        }

        halo.transform.position = targetRetPos.position;
    }

    void FixedUpdate()
    {
        Vector3 centerPos = new Vector3(0.5f, 0.5f, 0);
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(centerPos);

        if (Physics.Raycast(lazerPos.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (hit.transform.CompareTag("Zombie"))
            {
                particleSystem.Play();
                particleSystem.Simulate(10);
                particleSystem.playbackSpeed = 0.5f;
                lazerLight.enabled = true;
                halo.gameObject.SetActive(true); 
            }
            else
            {
                particleSystem.Clear();
                particleSystem.Stop();
                particleSystem.Simulate(0);
                particleSystem.playbackSpeed = 0.0f;
            }
        }
    }
}
