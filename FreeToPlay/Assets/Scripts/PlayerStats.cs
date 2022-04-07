using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    //Variables
    public GameManager gm;

    [Header("Attack Values")] 
    public int baseAttack = 10;
    public int attack;
    [Tooltip("Currently Unused")]
    public float block = 0.05f;

    [Header("Health Values")]
    public float health; //Current health
    public float baseMaxHealth = 5;
    public float maxHealth; //Max health

    [Header("Image Variables")] public GameObject healthSprite;
    public Sprite tankSprite;
    public Sprite mageSprite;


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

        healthSprite.transform.localScale = new Vector3(300*(health / maxHealth), healthSprite.transform.localScale.y, healthSprite.transform.localScale.z);


    }
    public void Attacked(int damage) //Taking damage
    {

        //Debug.Log("Player Hit Recieved");
        health -= damage; //Subtract the incoming damage value from health

    }

    public void Respawn() //Respawns player
    {
        gm.Invoke("ReloadScene", 0);

    }

    public void UpdateStats()
    {
        maxHealth = baseMaxHealth + FindObjectOfType<GameManager>().healthBonus;
        attack = baseAttack + FindObjectOfType<GameManager>().attackBonus;

        
    }

    public void NewRunInit()
    {
        switch (FindObjectOfType<GameManager>().characterType)
        {
            case 1: break;
            case 2: attack = Mathf.RoundToInt(attack * 0.7f);
                maxHealth = Mathf.RoundToInt(maxHealth * 1.25f);

                gameObject.GetComponent<SpriteRenderer>().sprite = tankSprite;
                break;
            case 3: attack = Mathf.RoundToInt(attack * 1.25f);
                maxHealth = Mathf.RoundToInt(maxHealth * 0.7f); 
            
                gameObject.GetComponent<SpriteRenderer>().sprite = mageSprite;
                break;
            default: break;
        }
        
       health = maxHealth;
    }
}
