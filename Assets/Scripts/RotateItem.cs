using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour
{
    private void Update()
    {
        // set rotation and speed 
        float speed = 0.5f; 
        transform.Rotate(transform.up, 25 * speed * Time.deltaTime);  
    }
}
