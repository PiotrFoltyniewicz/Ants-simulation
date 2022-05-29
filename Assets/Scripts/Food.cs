using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    int foodAmount = 5;

    public void TakeFood()
    {
        foodAmount--;
        if (foodAmount <= 0)
        {   
            FoodManager.foodList.Remove(transform);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ant")
        {
            if (collider.GetComponent<Ant>().currentState == 2) return;
            collider.GetComponent<Ant>().TouchedFood(transform);
            TakeFood();
        }
    }
}
