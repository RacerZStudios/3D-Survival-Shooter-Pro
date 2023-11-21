using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField]
    public Transform lazerPos;
    [SerializeField]
    public ParticleSystem enemyTracking;

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
            isActive = true;
            i += 1;
            if (i > 0 && i < 2)
            {
                halo.gameObject.SetActive(true);
                lazer.gameObject.SetActive(true);
                UpdateLazer();
                i += 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
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
        Ray ray = Camera.main.ScreenPointToRay(Vector3.forward); 
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.CompareTag("Zombie"))
            {
                enemyTracking.Play();
                enemyTracking.Simulate(10);
                enemyTracking.main.simulationSpeed.Equals(0.5f);
                enemyTracking.Emit(10);
                lazerLight.enabled = true;
                halo.gameObject.SetActive(true);
            }
            else
            {
                enemyTracking.Clear();
                enemyTracking.Stop();
                enemyTracking.Simulate(0);
                enemyTracking.main.simulationSpeed.Equals(0);
            }
        }
    }
}
