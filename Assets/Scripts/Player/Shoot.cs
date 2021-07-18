using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
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

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.name + "Hit");
                Health health = hit.collider.GetComponent<Health>();
                if(health != null)
                {
                    health.Damage(50);
                }
            }
        }
    }
}
