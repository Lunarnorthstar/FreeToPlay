using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health = 30;

    public int damage = 1;

    public int loot = 10;
 

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Attacked(int damage)
    {
        //Debug.Log("Enemy Hit Recieved");
        health -= damage;
    }
}
