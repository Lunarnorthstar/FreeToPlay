using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private UnityEvent<string> addGold;

    private Vector3 startPos;

    public int gold = 0;

    public void Start()
    {
        startPos = player.transform.position;
        gold = 0;
        UpdateUI();
        PauseGame();
    }

    public void GoldCollect(int input)
    {
        gold += input;
    }

    private void UpdateUI()
    {
        addGold.Invoke(gold.ToString());
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }
}
