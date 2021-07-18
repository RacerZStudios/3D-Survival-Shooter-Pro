using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDamping : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed = 10f;

    private void LateUpdate()
    {
        Vector3 targetPos = target.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
        transform.rotation = Quaternion.Euler(target.transform.rotation.eulerAngles); 
    }
}
