using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this needs to sit an initial position until projectile moves. and then we want the camera to stop at a certain boundary and declare that  
//the projectile is no longer worth following
public class FollowCam : MonoBehaviour
{
    static public GameObject POI; //static so the same value is shared by all instances of the this FollowCam class. 

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ;// The desired Z pos of the camera

    void Awake() 
    {
        camZ = this.transform.position.z;
            }

    void FixedUpdate()
    {
        if (POI == null) return;// if no poi, exit f.update.

        Vector3 destination = POI.transform.position;

        //limit minimum values:
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        destination = Vector3.Lerp(transform.position, destination, easing); //interpolate from camera position toward destination with some easing.
        destination.z = camZ;
        transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10; //set size of camera to keep ground in view.
    }


}
