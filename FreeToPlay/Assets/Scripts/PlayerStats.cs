using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    //Variables

    [Header("Attack Values")]
    public int attack = 10;
    public float block = 0.05f;

    [Header("Health Values")]
    public int health; //Current health
    public int maxHealth; //Max health

    [Header("Image Variables")]
    public Image[] hearts; //Array of heart images
    public Sprite fullHeart; //image when full
    public Sprite emptyHeart; //Image when empty


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) //If health is less than or equal to 0 (none)
        {
            Respawn(); //Respawen the player
        }
        if (health > maxHealth) //If health somehow is pushed over the max
        {
            health = maxHealth; //Set it to the max
        }

        for (int i = 0; i < hearts.Length; i++) //For the length of the array
        {
            if (i < health) //if array slot less than current health
            {
                hearts[i].sprite = fullHeart; //make a fullheart sprite in the array
            }
            else //if it isnt
            {
                hearts[i].sprite = emptyHeart; //Make an empty heart in the array
            }

            if (i < maxHealth) //If the array slot is less than max
            {
                hearts[i].enabled = true; //keep enabling hearts
            }
            else //if it isnt
            {
                hearts[i].enabled = false; //dont enable any more hearts
            }

        }

    }
    public void DealDamage() //Deals damage
    {
        
            health--; //Lower health by 1

    }

    public void Respawn() //Respawns player
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }
}
