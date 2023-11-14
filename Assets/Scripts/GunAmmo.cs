using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;

public class GunAmmo : MonoBehaviour
{
    public GameObject shoot;
    public GameObject gun;
    [SerializeField]
    public int Ammo = 10;
    [SerializeField]
    public Text ammoText; 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ammo--;
            ammoText.text = Ammo.ToString() + " : Total Ammo Count "; 
        }

        if(Ammo <= 0)
        {
            Ammo = 0; 
            shoot.GetComponent<Shoot>().enabled = false;
            gun.GetComponent<Barreta_Pistol_Fire>().enabled = false;
        }
        else if(Ammo > 0)
        {
            shoot.GetComponent<Shoot>().enabled = true;
            gun.GetComponent<Barreta_Pistol_Fire>().enabled = true;
        }
    }
}
