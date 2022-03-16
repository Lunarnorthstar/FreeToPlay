using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [Tooltip("How much gold is added to the total when collected")]
    public int value = 0;
    public void Worth(int input)
    {
        value = input; //Update your value based on the input from another gameobject.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            FindObjectOfType<Collection>().SendMessage("GoldCollect", value); //Find the game manager and update the gold value
            Destroy(gameObject); //Delete self.
=======
=======
>>>>>>> Stashed changes
            FindObjectOfType<GameManager>().SendMessage("GoldCollect", value);
            Destroy(gameObject);
>>>>>>> Stashed changes
        }
    }
}
