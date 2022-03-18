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

    private Vector3 startPos;

    public int gold;
    public int healthBonus;
    public int attackBonus;

    public float attackCost = 5;
    public float healthCost = 5;

    public int attackIncrease = 1;
    public int healthIncrease = 1;


    public float multipier = 1.2f;

    public void Update()
    {
        //GetState();
        UpdateUI();
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
    
        player = GameObject.FindGameObjectWithTag("Player");
        GetState();
        UpdateUI();
    }

    public void GoldCollect(int input)
    {
        gold += input;
        UpdateUI();
    }

    private void UpdateUI()
    {
        addGold.Invoke(gold.ToString());
        attackPrice.Invoke(attackCost.ToString());
        healthPrice.Invoke(healthCost.ToString());
        attackBonusadd.Invoke(attackBonus.ToString());
        healthBonusadd.Invoke(healthBonus.ToString());
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

    public void ResetGame()
    {
        attackBonus = 0;
        healthBonus = 0;
        gold = 0;
        attackCost = 5;
        healthCost = 5;
        SetState();
    }
    public void SetState()
    {
      
        PlayerPrefs.SetInt("attackBonus", attackBonus);
        PlayerPrefs.SetInt("healthBonus", healthBonus);
        PlayerPrefs.SetFloat("attackCost", attackCost);
        PlayerPrefs.SetFloat("healthCost", healthCost);
        PlayerPrefs.SetInt("coins", gold);
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
}
