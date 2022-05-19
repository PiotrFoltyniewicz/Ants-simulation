using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    int foodAmount = 10;

    public void TakeFood()
    {
        foodAmount--;
        if(foodAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
