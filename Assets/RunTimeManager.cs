using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimeManager : MonoBehaviour
{
    [SerializeField]
    public Shoot playerActions;

    [SerializeField]
    public Barreta_Pistol_Fire barreta;

    [SerializeField]
    public GunAmmo gunAmmo; 

    [SerializeField]
    public PauseGame paused;

    public void PlayerAction()
    {
        playerActions.enabled = true;
        barreta.enabled = true;
        gunAmmo.enabled = true;         
    }

    private void Update()
    {
        if (paused.pauseMenuActive == true)
        {
            playerActions.enabled = false;
            barreta.enabled = false;
            gunAmmo.enabled = false;
        }
    }
}
