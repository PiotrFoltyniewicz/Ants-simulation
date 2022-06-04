using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * klasa Food punkty jedzenia ktore sa zabierane przez mrowki
 */
public class Food : MonoBehaviour
{
    int foodAmount = (int)Variables.GetVariable("foodHealth"); // 'zycie' jedzenia

    // metoda odpowiadajaca za podniesienie jedzenia przez mrowke
    public void TakeFood()
    {
        foodAmount--;
        // jezeli wartosc foodAmount <= 0 to punkt znika z mapy
        if (foodAmount <= 0)
        {
            FoodManager.foodList.Remove(transform);
            gameObject.SetActive(false);
        }
    }
    // metoda odpowiadajaca za dzialanie kiedy mrowka dotknie jedzenia
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
