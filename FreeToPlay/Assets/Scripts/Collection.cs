using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    public int gold = 0;

    public void GoldCollect(int input)
    {
        gold += input;
    }
}
