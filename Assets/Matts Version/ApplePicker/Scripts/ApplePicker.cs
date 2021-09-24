using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{

    [Header("Set in Inspector")]
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;

    public List<GameObject> basketList;

    void Start()
    {
        //create multiple baskets
        for (int i = 0; i < numBaskets; i++)
        {
            basketList = new List<GameObject>();
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }


    }
        //This destroys all remaining apples on screen to give a game over
        //this happened because an apple got bottom of screen.
     public void AppleDestroyed()
        {
            // Destroy all of the falling apples

            //gets an array of all the apples. 
            GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");

            //iterate through array destroy all apples
            foreach (GameObject tGO in tAppleArray)
            {
                Destroy(tGO);
            }



            // Destroy one of the baskets                                     

            // Get the index of the last Basket in basketList
            int basketIndex = basketList.Count - 1;

            // Get a reference to that Basket GameObject
            GameObject tBasketGO = basketList[basketIndex];

            // Remove the Basket from the list and destroy the GameObject
            basketList.RemoveAt(basketIndex);
            Destroy(tBasketGO);

            //no baskets left
            if (basketList.Count == 0)
                {
                SceneManager.LoadScene("Scene1_ApplePicker");
                }
        }
    }