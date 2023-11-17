using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
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

    [SerializeField]
    private GameObject disableWhenDead; 

    private void Awake()
    {
        imageEffect = GameObject.Find("Image").GetComponent<Image>();
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
        if(damageAmount < currentHealh && this.gameObject.CompareTag("Player"))
        {
            isPlayerHit = true;
        }
        else if(currentHealh <= maxHealth)
        {
            isPlayerHit = false; 
        }

        if (isPlayerHit == true && this.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeColor());
        }
        else if (isPlayerHit == false && this.gameObject.CompareTag("Player"))
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

                if(this.gameObject.CompareTag("Player") && isDead == true)
                {
                    GameObject.Find("Player").GetComponent<CharacterController>().enabled = false;
                    GameObject.Find("Baretta").GetComponent<Barreta_Pistol_Fire>().enabled = false;

                    // restart game / load level 
                    // add delay time to load scene for player animation dead 
                    StartCoroutine(LoadEndScene()); 
                    // spawn restart ui 
                }
                // player dead 
            }
        }
    }

    private IEnumerator LoadEndScene()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadSceneAsync(2);
        yield return null; 
    }

    private IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(8);
        this.gameObject.GetComponentInParent<EnemyAI>().enabled = false; 
        this.gameObject.GetComponentInChildren<AttackTrigger>().GetComponent<AttackTrigger>().enabled = false; 
        this.gameObject.GetComponentInChildren<AttackTrigger>().GetComponent<SphereCollider>().enabled = false;
        this.gameObject.GetComponentInChildren<AttackTrigger>().GetComponent<CapsuleCollider>().enabled = false;
        this.gameObject.GetComponentInParent<CharacterController>().detectCollisions = false;
        this.gameObject.GetComponentInParent<NavMeshAgent>().enabled = false;
        this.gameObject.GetComponentInParent<CharacterController>().enabled = false;
        anim.enabled = false;
    }

    private IEnumerator FadeColor()
    {
        // Debug.Log("Taking Damage");
       // isPlayerHit = true; 
        imageEffect.CrossFadeAlpha(0, 0, true);
        imageEffect.fillAmount = 30;
        imageEffect.color = new Color(255, 113, 0, 0);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(StopFadeColor()); 
    }

    private IEnumerator StopFadeColor()
    {
       // isPlayerHit = false; 
        imageEffect.fillAmount = 0;
        imageEffect.color = new Color(5, 0, 0, 1);
        imageEffect.CrossFadeAlpha(0.1f, 0.1f, false);
        yield return new WaitForSeconds(0.5f); 
        imageEffect.CrossFadeAlpha(0, 0, true);
        imageEffect.fillAmount = 30;
        imageEffect.color = new Color(255, 113, 0, 0);
    }
}