using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    //bottom of screen
    public static float bottomY = -20f; 

    void Update()
    {
        if (transform.position.y < bottomY)
        {
            //remove
            Destroy(this.gameObject);
        }
    }
}