using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private UnityEvent<string> addGold;
    [SerializeField] private UnityEvent<string> attackPrice;
    [SerializeField] private UnityEvent<string> healthPrice;
    [SerializeField] private UnityEvent<string> attackBonusadd;
    [SerializeField] private UnityEvent<string> healthBonusadd;
    [SerializeField] private UnityEvent<string> timerAdd;
    [SerializeField] private UnityEvent<string> onlineAdd;


    public bool dayGoldGet;
    public bool dayHealthGet;
    public bool dayEnemyGet;
    public bool weekGoldGet;
    public bool weekHealthGet;
    public bool weekEnemyGet;

    private Vector3 startPos;

    public QuestTracker quests;

    public int gold;
    public int healthBonus;
    public int attackBonus;

    public float attackCost = 5;
    public float healthCost = 5;

    public int attackIncrease = 1;
    public int healthIncrease = 1;

    public int characterType = 1;


    public float multipier = 1.2f;

    public float dayTimer;
    public float onlineTimer;

    public void Update()
    {
        //GetState();
        UpdateUI();

        
        
        
       
    }
    public void FixedUpdate()
    {
        if (dayTimer > 0)
        {
            dayTimer -= Time.deltaTime;
        }

        onlineTimer -= Time.deltaTime;
    }

    public void Start()
    {
        startPos = player.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        GetState();
        //UpdateUI();
        //PauseGame();
    }

    public void Awake()
    {
        if (PlayerPrefs.GetFloat("attackCost") == 0)
        {
            PlayerPrefs.SetFloat("attackCost", 5);
        }
        
        if (PlayerPrefs.GetFloat("healthCost") == 0)
        {
            PlayerPrefs.SetFloat("healthCost", 5);
        }

        if(dayTimer <= 0)
        {
            dayEnemyGet = false;
            dayGoldGet = false;
            dayHealthGet = false;

            weekEnemyGet = false;
            weekGoldGet = false;
            weekHealthGet = false;
        }
    
        player = GameObject.FindGameObjectWithTag("Player");
        GetState();
        UpdateUI();
    }

    public void CountDown()
    {
        if (dayTimer <= 0)
        {
            dayTimer = 86400; //seconds in a day
        }
    }

    public void GoldCollect(int input)
    {
        gold += input;
        UpdateUI();
    }

    public void GoldCollectTimer(int input)
    {
        if (dayTimer <= 0)
        {
            gold += input;
            UpdateUI();
        }
    }

    public void GoldCollectOnline(int input)
    {
        if (onlineTimer <= 0)
        {
            gold += input;
            onlineTimer += 300;
            UpdateUI();
        }
    }


    public void dayGold()
    {
        
        if(quests.coinsCollected >= 200)
        {
            
        }

        UpdateUI();
    }
    public void dayHealth()
    {

        if (quests.healthCollected >= 25)
        {

        }

        UpdateUI();
    }
    public void dayEnemy()
    {

        if (quests.coinsCollected >= 30)
        {

        }

        UpdateUI();
    }


    private void UpdateUI()
    {
        


        addGold.Invoke(gold.ToString());
        attackPrice.Invoke(attackCost.ToString());
        healthPrice.Invoke(healthCost.ToString());
        attackBonusadd.Invoke(attackBonus.ToString());
        healthBonusadd.Invoke(healthBonus.ToString());
        timerAdd.Invoke(Mathf.RoundToInt(dayTimer).ToString() + " seconds");
        onlineAdd.Invoke(Mathf.RoundToInt(onlineTimer).ToString() + " seconds");
        if(onlineTimer <= 0)
        {
            onlineAdd.Invoke("Ready!");
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        UpdateUI();
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    public void ReloadScene()
    {

        SetState();

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        //GetState(); 
        UpdateUI();
    }

    public void OnApplicationQuit()
    {
        SetState();
    }

    public void ResetGame()
    {
        attackBonus = 0;
        healthBonus = 0;
        gold = 0;
        attackCost = 5;
        healthCost = 5;

        quests.healthCollected = 0;
        quests.enemiesDefeated = 0;
        quests.coinsCollected = 0;

        dayTimer = 0;
        onlineTimer = 0;
        SetState();
    }
    public void SetState()
    {
      
        PlayerPrefs.SetInt("attackBonus", attackBonus);
        PlayerPrefs.SetInt("healthBonus", healthBonus);
        PlayerPrefs.SetFloat("attackCost", attackCost);
        PlayerPrefs.SetFloat("healthCost", healthCost);
        PlayerPrefs.SetInt("coins", gold);
        PlayerPrefs.SetFloat("dayTimer", dayTimer);
        PlayerPrefs.SetFloat("onlineTime", onlineTimer);
        //now we need to save the data to PlayerPrefs on disk 
        PlayerPrefs.Save();
    }
    public void GetState()
    {

        attackBonus = PlayerPrefs.GetInt("attackBonus");
        healthBonus = PlayerPrefs.GetInt("healthBonus");
        attackCost = PlayerPrefs.GetFloat("attackCost");
        healthCost = PlayerPrefs.GetFloat("healthCost");
        gold = PlayerPrefs.GetInt("coins");
        dayTimer = PlayerPrefs.GetFloat("dayTimer");
        onlineTimer = PlayerPrefs.GetFloat("onlineTime");
       
    }
    public void UpgradeAttack()
    {
        if (gold >= attackCost)
        {
            attackBonus += attackIncrease;
            gold -= Mathf.RoundToInt(attackCost);
            attackCost = Mathf.RoundToInt(attackCost * multipier);
            SetState();
        }
    }
    public void UpgradeHealth()
    {
        if (gold >= healthCost)
        {
            healthBonus += healthIncrease;
            gold -= Mathf.RoundToInt(healthCost);
            healthCost = Mathf.RoundToInt(healthCost * multipier);
            SetState();
        }
    }

    public void SetClass(int input)
    {
        characterType = input;
    }
}
