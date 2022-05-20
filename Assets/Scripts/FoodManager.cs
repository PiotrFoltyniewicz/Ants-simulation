using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    int foodAmount = 50;
    float foodSpawnRadius = 0.5f;
    GameObject food; // GameObject jedzenia
    Transform foodPoint; // Punkt centrum spawnu jedzenia
    void Awake()
    {
        CreateFoodPoint();
        CreateFoodGameObject();
    }

    private void Start()
    {
        for (int i = 0; i < foodAmount; i++)
        {
            Vector2 foodPos = (Vector2)foodPoint.position + (Random.insideUnitCircle * foodSpawnRadius);
            SpawnFood(foodPos);
        }
    }

    // spawn jedzenia
    void SpawnFood(Vector2 position)
    {
        GameObject temp = Instantiate(food, position, Quaternion.identity);
        temp.AddComponent<Food>();
        temp.SetActive(true);
    }

    // tworzenie GameObjectu jedzenia
    void CreateFoodGameObject()
    {
        food = new GameObject();
        food.SetActive(false);
        food.AddComponent<SpriteRenderer>();
        food.AddComponent<CircleCollider2D>().isTrigger = true;
        food.GetComponent<CircleCollider2D>().radius = 0.06f;
        food.name = "Food";
        food.tag = "Food";
        // dodawanie tekstur do mrï¿½wki
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
        foodPoint.position = new Vector2(Random.Range(-8, 8), Random.Range(-4, 4));
    }

}