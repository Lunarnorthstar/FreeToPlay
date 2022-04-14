using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    public GameManager gm;
    public QuestTracker quests;
    public Button myButton;

    public bool goldQuest;
    public bool healthQuest;
    public bool enemyQuest;
    public bool dailyQuest;
    public bool weeklyQuest;

    public int questNeeds;


    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
    }

    private void Update()
    {
        if(gm.dayTimer <= 0)
        {
            Debug.Log("RUN");
            myButton.interactable = true;
        }
    }

    // Update is called once per frame
    public void DisableButton()
    {
        if (dailyQuest)
        {
            if((goldQuest && quests.coinsCollected >= questNeeds) || 
               (healthQuest && quests.healthCollected >= questNeeds) ||
               (enemyQuest && quests.enemiesDefeated >= questNeeds))
            {
            
                myButton.interactable = false;
                gm.gold += gm.dailyReward;
                gm.Invoke("UpdateUI",0); 
                
            }
        }
        
        if (weeklyQuest)
        {
            if((goldQuest && (quests.coinsCollectedWeek + quests.coinsCollected) >= questNeeds) || 
               (healthQuest && quests.healthCollectedWeek + quests.healthCollected >= questNeeds) ||
               (enemyQuest && quests.enemiesDefeatedWeek + quests.enemiesDefeated >= questNeeds))
            {
            
                myButton.interactable = false;
                gm.gold += gm.weeklyReward;
                gm.Invoke("UpdateUI",0); 
                
            }
        }
        
        
        
        
    }

    public void EnableButton()
    {
        myButton.interactable = true;
    }
}
