using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Info")]
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int minHealth = 0;
    [SerializeField]
    protected int currentHealh;

    private void Start()
    {
        maxHealth = currentHealh; 
    }

    // damage (int damageAmount) 
    // take damage 
    // current health - damage amount
    // check if dead 
    // if current health - < min health 
    // destroy 

    public void Damage(int damageAmount)
    {
        currentHealh -= damageAmount; 
        if(currentHealh <= minHealth)
        {
            Debug.Log(gameObject.name + ToString()); 
            Destroy(gameObject); 
        }
    }
}
