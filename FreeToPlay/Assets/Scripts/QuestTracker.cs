using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObject/QuestProgressTracker", order = 1)]

public class QuestTracker : ScriptableObject
{
    public int coinsCollected;
    public int healthCollected;
    public int enemiesDefeated;
    
    public int coinsCollectedWeek;
    public int healthCollectedWeek;
    public int enemiesDefeatedWeek;

}
