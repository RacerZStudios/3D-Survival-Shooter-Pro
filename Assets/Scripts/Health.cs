using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Info")]
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int minHealth;
    [SerializeField]
    private int currentHealh;

    private void Start()
    {
        currentHealh = maxHealth; 
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
            Destroy(gameObject); 
        }
    }
}
