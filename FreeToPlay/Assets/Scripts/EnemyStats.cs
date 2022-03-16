using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Stats")]
    public int health = 30;
    public int damage = 1;

    [Header("Loot Drop Variables")]
    public int loot = 10;
    public GameObject lootDrop;

    [Header("Health Drop Variables")]
    [Tooltip("From 1 to 100, expressed as a percentage")]
    public float healthDropChance = 50;
    public GameObject healthDrop;
    public int healthRestore = 3;


    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GameObject lootSpawn = Instantiate(lootDrop, transform.position, transform.rotation);
            lootSpawn.SendMessage("Worth", loot);
            if (Random.Range(0, 100) <= healthDropChance)
            {   
                GameObject healthSpawn = Instantiate(healthDrop, transform.position, transform.rotation);
                healthSpawn.SendMessage("Value", healthRestore);
            }
            
            Destroy(gameObject);
        }
    }

    public void Attacked(int damage)
    {
        //Debug.Log("Enemy Hit Recieved");
        health -= damage;
    }
}
