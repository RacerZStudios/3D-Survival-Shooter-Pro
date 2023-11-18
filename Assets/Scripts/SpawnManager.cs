using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI; 
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    public GameObject spawnPos;
    // Global amout of Zombies to spawn in level 
    public static int zombiesToSpawn;
    public int amount = 13; 
    public GameObject zombiesToKill; 

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(spawnPos, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        zombiesToSpawn = amount;
        zombiesToKill.GetComponent<Text>().text = " Zombies to Kill = 13 ";

        StartCoroutine(FindObjects()); 

        if(this.gameObject.activeInHierarchy)
        {
            StopCoroutine(FindObjects());
            // Declared Destroy method removes any data stored in memory after Update() 
            Destroy(this);
        }
    }

    private IEnumerator FindObjects()
    {
        foreach (var go in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (go.name == "New Game Object")
            {
                if(this.gameObject.activeInHierarchy)
                {
                    Destroy(go);
                }
            }
        }
        yield break;  
    }
}