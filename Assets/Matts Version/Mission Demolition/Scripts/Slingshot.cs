using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    static private Slingshot S; //singleton of self


    [Header("Set in Inspector")]
    public GameObject prefabProjectile; //Projectile prefab cached to instantiate
    public float velocityMult = 8f;

    [Header("Set Dynamically")]
    public GameObject launchPoint; //Halo - signal launch point mouse over stuff
    public Vector3 launchPos; //position of launch
    public GameObject projectile;
    public bool aimingMode; //firing state
    private Rigidbody projectileRigidbody;


    //Properties:
    public static Vector3 LAUNCH_POS //no set, so public read access only.
    {
        get
        {
            if (S == null) return Vector3.zero;
            return S.launchPos;
        }
    }
  

    private void Awake()
    {
        S = this;
        Transform launchPointTrans = transform.Find("LaunchPoint"); //temp cache transform of launchpoint
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position; // assign transform of launchpoint found above
    }

    private void Update()
    {
        if (!aimingMode) return; //drop update if not in aiming mode state

        Vector3 mousePos2d = Input.mousePosition;
        mousePos2d.z = -Camera.main.transform.position.z;
        Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(mousePos2d);

        Vector3 mouseDelta = mousePos3d - launchPos;

        //Limit mouseDelta to the radios of Slingshot SphereCollider:
        float maxMagnitude = this.GetComponent <SphereCollider>().radius;

        if(mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        //move projectile:
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        //look for mouse button release
        if (Input.GetMouseButtonUp(0))
            {
            aimingMode = false; //change state to false
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;

            FollowCam.POI = projectile;
            projectile = null;
            }
    }



    private void OnMouseDown()
    {
        aimingMode = true; //set aiming state
        projectile = Instantiate(prefabProjectile) as GameObject; //create projectile
        projectile.transform.position = launchPos;//place projectile
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true; //find rigid body and add kinematic
                                                //kinematic stops physics forces from acting upon it?
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
