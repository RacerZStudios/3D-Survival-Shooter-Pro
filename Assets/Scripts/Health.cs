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

    private Animator anim;
    public bool isDead = false; 

    private void Start()
    {
        maxHealth = currentHealh;
        anim = GetComponentInParent<Animator>(); 
    }

    // damage (int damageAmount) 
    // take damage 
    // current health - damage amount
    // check if dead 
    // if current health - < min health 
    // destroy 

    // if health < 10 and != 0
    // find health packs and restore health++ maxHealth 

    public void Damage(int damageAmount)
    {
        currentHealh -= damageAmount; 
        if(currentHealh <= minHealth)
        {
            isDead = true; 
            // Debug.Log(gameObject.name + ToString()); 
            if(isDead == true)
            {
                anim.SetTrigger("Dead");
                anim.SetBool("Idle", false);
                StartCoroutine(WaitToDisable()); 
                if(gameObject.tag == "Player" && isDead == true)
                {
                    GameObject.Find("Player").GetComponent<CharacterController>().enabled = false;
                    GameObject.Find("Baretta").GetComponent<Barreta_Pistol_Fire>().enabled = false; 
                }
              //  Destroy(gameObject, 12); // player dead 
            }
           // restart game / load level 
           // spawn restart ui 
        }
    }

    private IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(8);
        CharacterController controller = GetComponentInParent<CharacterController>();
        controller.enabled = false;
        anim.enabled = false;
    }
}