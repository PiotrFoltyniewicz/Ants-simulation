using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * klasa FoodManager zarzadza systemem tworzenia jedzenia na mapie
 */
public class FoodManager : MonoBehaviour
{
    int foodAmount = (int)Variables.GetVariable("amountOfFoodInPoint"); // ilosc mniejszych zielonych punktow w okregu
    float foodSpawnRadius = (float)Variables.GetVariable("foodSpawnRadiusInPoint"); // zasieg pojawiania sie mniejszych zielonych punktow w okregu
    GameObject food; // GameObject jedzenia
    Transform foodPoint; // punkt centrum spawnu jedzenia
    int foodPointAmount = (int)Variables.GetVariable("amountOfFoodPoints"); // ilosc punktow wookol ktorych pojawiaja sie mniejsze zielone punkty jedzeia
    public static List<Transform> foodList = new List<Transform>(); // lista przechowujaca punkty jedzenia

    void Awake()
    {
        CreateFoodGameObject();
    }

    private void Start()
    {
        for (int i = 0; i < foodPointAmount; i++)
        {
            CreateFoodPoint();
            for (int j = 0; j < foodAmount; j++)
            {
                Vector2 foodPos = (Vector2)foodPoint.position + (Random.insideUnitCircle * foodSpawnRadius);
                SpawnFood(foodPos);
            }
        }
    }

    // spawn jedzenia
    void SpawnFood(Vector2 position)
    {
        GameObject temp = Instantiate(food, position, Quaternion.identity);
        foodList.Add(temp.transform);
        temp.SetActive(true);
    }

    // tworzenie GameObjectu jedzenia
    void CreateFoodGameObject()
    {
        food = new GameObject();
        food.SetActive(false);
        food.AddComponent<SpriteRenderer>();
        food.AddComponent<Food>();
        food.AddComponent<CircleCollider2D>().isTrigger = true;
        food.GetComponent<CircleCollider2D>().radius = 0.06f;
        food.name = "Food";
        food.tag = "Food";
        // dodawanie tekstur do mrowki
        Texture2D foodTexture = (Texture2D)Resources.Load("Textures/Circle");
        Sprite foodSprite = Sprite.Create(foodTexture, new Rect(0f, 0f, foodTexture.width, foodTexture.height), new Vector2(0.5f, 0.5f), 2048);
        food.GetComponent<SpriteRenderer>().sprite = foodSprite;
        food.GetComponent<SpriteRenderer>().color = Color.green;
    }

    // tworzenie punktu dookola ktorego bedzie sie pojawiac jedzenie
    void CreateFoodPoint()
    {
        foodPoint = new GameObject().transform;
        foodPoint.name = "FoodPoint";
        foodPoint.position = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
    }

}