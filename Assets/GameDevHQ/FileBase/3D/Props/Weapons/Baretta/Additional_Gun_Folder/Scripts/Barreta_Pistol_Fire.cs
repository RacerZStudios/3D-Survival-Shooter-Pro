using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barreta_Pistol_Fire : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _smoke;

    [SerializeField]
    private ParticleSystem _bulletCasing;

    [SerializeField]
    private ParticleSystem _muzzleFlashSide;

    [SerializeField]
    private ParticleSystem _Muzzle_Flash_Front;

    private Animator _anim;

    [SerializeField]
    private AudioClip _gunShotAudioClip;

    [SerializeField]
    private AudioSource _audioSource;

    public bool fullAuto = false;
    public bool semiAuto = true;  
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            semiAuto = true;
            _anim.SetBool("Automatic_Fire", false);
            FireGunParticles();
        }
        else if(Input.GetKey(KeyCode.Mouse0))
        {
            fullAuto = true; 
            if(fullAuto == true)
            {
                semiAuto = false;
                _anim.SetBool("Automatic_Fire", true);
                FireGunParticlesFullAuto(); 
            }
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            fullAuto = false;
            semiAuto = false;
            _anim.SetBool("Automatic_Fire", false);
        }
        else
        {
            _anim.SetTrigger("Idle"); 
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            _anim.SetTrigger("Reload"); 
        }
    }

    public void FireGunParticles()
    {
        if(fullAuto == false)
        {
            _smoke.Play();
            _bulletCasing.Play();
            _muzzleFlashSide.Play();
            _Muzzle_Flash_Front.Play();
            GunFireAudio();
        }    
    }

    public void FireGunParticlesFullAuto()
    {
        if (fullAuto == true)
        {
            _smoke.Play();
            _bulletCasing.Play();
            _muzzleFlashSide.Play();
            _Muzzle_Flash_Front.Play();
            GunFireAudioFull(); 
        }
    }

    public void GunFireAudio()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(_gunShotAudioClip);
    }

    public void GunFireAudioFull()
    {
        _audioSource.pitch = Random.Range(0.3f, 1.4f);
        _audioSource.PlayDelayed(delay:0.15f);
    }
}