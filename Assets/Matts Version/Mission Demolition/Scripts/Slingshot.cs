using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    
    [Header("Set in Inspector")]
    public GameObject prefabProjectile; //Projectile prefab cached to instantiate


    [Header("Set Dynamically")]
    public GameObject launchPoint; //Halo - signal launch point mouse over stuff
    public Vector3 launchPos; //position of launch
    public GameObject projectile;
    public bool aimingMode; //firing state

    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint"); //temp cache transform of launchpoint
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position; // assign transform of launchpoint found above
    }

    private void OnMouseDown()
    {
        aimingMode = true; //set aiming state
        projectile = Instantiate(prefabProjectile) as GameObject; //create projectile
        projectile.transform.position = launchPos;//place projectile
        projectile.GetComponent<Rigidbody>().isKinematic = true; //find rigid body and add kinematic.
    }

    //activate launch point when mouse enters
    private void OnMouseEnter()
    {
        launchPoint.SetActive(true);
    }
    //deactive launch point when mouse exits
    private void OnMouseExit()
    {
        launchPoint.SetActive(false);
    }
}
