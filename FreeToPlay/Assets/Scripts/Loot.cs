using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public int value = 0;
    public void Worth(int input)
    {
        value = input;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("U");
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<Collection>().SendMessage("GoldCollect", value);
            Destroy(gameObject);
        }
    }
}
