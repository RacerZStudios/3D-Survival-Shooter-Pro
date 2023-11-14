using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
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
    public bool isPlayerHit = false; 

    [SerializeField]
    private Image imageEffect;

    private void Awake()
    {
        imageEffect = GetComponent<Image>(); 
    }

    private void Start()
    {
        maxHealth = currentHealh;
        anim = GetComponentInParent<Animator>(); 
    }

    private void LateUpdate()
    {
        if(imageEffect == null)
        {
            imageEffect = FindObjectOfType<Image>();
            return; 
        }
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
        isPlayerHit = true;
        if (isPlayerHit == true && this.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeColor());
        }
        else if (isPlayerHit == false)
        {
            isPlayerHit = false;
            StopCoroutine(FadeColor()); 
        }
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
                // player dead 
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
        gameObject.GetComponentInChildren<AttackTrigger>().GetComponent<SphereCollider>().enabled = false;
    }

    private IEnumerator FadeColor()
    {
        // Debug.Log("Taking Damage");
        imageEffect.CrossFadeAlpha(0, 0, true);
        imageEffect.fillAmount = 30;
        imageEffect.color = new Color(255, 113, 0, 0);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(StopFadeColor()); 
    }

    private IEnumerator StopFadeColor()
    {
        imageEffect.fillAmount = 0;
        imageEffect.color = new Color(5, 0, 0, 1);
        imageEffect.CrossFadeAlpha(0.1f, 0.1f, false);
        yield return new WaitForSeconds(0.5f); 
        imageEffect.CrossFadeAlpha(0, 0, true);
        imageEffect.fillAmount = 30;
        imageEffect.color = new Color(255, 113, 0, 0);
    }
}