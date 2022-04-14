using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [Tooltip("How much gold is added to the total when collected")]
    public int value = 0;

    public QuestTracker quests;
    public void Worth(int input)
    {
        value = input; //Update your value based on the input from another gameobject.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {

            quests.coinsCollected += value;
            FindObjectOfType<GameManager>().SendMessage("GoldCollect", value);
            Destroy(gameObject);

        }
    }
}
