using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    public GameObject spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(spawnPos, transform.position, Quaternion.identity);
    }
}
