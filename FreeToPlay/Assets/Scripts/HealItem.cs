using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    private int value = 0;

    public void Value(int input)
    {
        value = input;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStats>().health += value;
            Destroy(gameObject);
        }
    }
}
