using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private EnemyAI ai;

    private void Start()
    {
        ai = GetComponentInParent<EnemyAI>(); 

        if(ai == null)
        {
            Debug.LogError("The Enemy AI is Null"); 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            // In Attack State 
            ai.StartAttack(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            // In Attack State 
            ai.StopAttack(); 
        }
    }
}
