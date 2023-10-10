using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    [SerializeField]
    public GameObject objectPos;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(objectPos, transform.position, Quaternion.identity);
    }
}
