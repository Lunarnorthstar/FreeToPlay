using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    [Tooltip("How much health this adds to the total when collected")]
    private int value = 0;

    public void Value(int input)
    {
        value = input; //Update value based on the number received from the object that calls this function.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStats>().health += value; //Increase the health stat of the other object.
            Destroy(gameObject); //Destroy self.
        }
    }
}
