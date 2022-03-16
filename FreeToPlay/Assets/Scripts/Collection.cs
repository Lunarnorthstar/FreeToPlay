using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    [Tooltip("The amount of gold this object is worth")]
    [SerializeField]private int gold = 0;

    public void GoldCollect(int input) //When this function is called...
    {
        gold += input; //The value of this item is updated.
    }
}
