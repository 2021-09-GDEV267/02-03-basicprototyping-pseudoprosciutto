using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]

    // Start is called before the first frame update

    public GameObject applePrefab;

    //speed at which tree moves
    public float speed = 1f;

    public float leftAndRightEdge = 30f;

    public float chanceToChangeDirections = .02f;

    public float secondsBetweenAppleDrops = 1f;

    void Start()
        //dropping apples every second
    {
        Invoke("DropApple", 2f);                                      
    }

    void DropApple()
    {
        GameObject apple = Instantiate<GameObject>(applePrefab);      

        apple.transform.position = transform.position;                

        Invoke("DropApple", secondsBetweenAppleDrops);
    }


    // Update is called once per frame
    void Update()
    {
        //Basic Movement
        Vector3 pos = transform.position;

        pos.x += speed * Time.deltaTime;

        transform.position = pos;

        //Changing Direction
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);// Move right
        } else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed);// Move left
        }
    }
    void FixedUpdate(){
        if (Random.value < chanceToChangeDirections)
        { speed *= -1;    // change direction
         }
    }
}
