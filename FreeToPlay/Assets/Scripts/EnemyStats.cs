using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStats : MonoBehaviour
{
    [Header("Stats")]
    public int health = 30;
    public int damage = 1;

    [Header("Loot Drop Variables")]
    [Tooltip("The value of the gold item dropped by the enemy")]
    public int loot = 10;
    public GameObject lootDrop;

    [Header("Health Drop Variables")]
    [Tooltip("From 1 to 100, expressed as a percentage")]
    public float healthDropChance = 50;
    public GameObject healthDrop;
    [Tooltip("The amount of health the health drop restores")]
    public int healthRestore = 3;
    
    private float timer = 0.2f;
    private Color temp;

    private void Start()
    {
        temp = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) //If your health is 0 or less...
        {
            GameObject lootSpawn = Instantiate(lootDrop, transform.position, transform.rotation); //Spawn a loot prefab at your current location.
            lootSpawn.SendMessage("Worth", loot); //Tell the loot prefab how much it's worth.
            if (Random.Range(0, 101) <= healthDropChance) //Generate a number from 1 to 100 (minimum is inclusive, maximum is exclusive, don't ask why) and if it's less than the drop chance for health items...
            {   
                GameObject healthSpawn = Instantiate(healthDrop, transform.position, transform.rotation); //Generate a healing item prefab at your current location.
                healthSpawn.SendMessage("Value", healthRestore); //Tell the healing item prefab how much it's worth.
            }
            
            Destroy(gameObject); //Destroy self.
        }


        if (timer <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = temp;
        }

        timer -= Time.deltaTime;
    }

    public void Attacked(int damage)
    {
        //Debug.Log("Enemy Hit Recieved");
        health -= damage; //Lose health based on the damage sent by the attacking object, which calls this function.



        
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        timer = 0.2f;
    }
}
