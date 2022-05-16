using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    GameObject food; // GameObject jedzenia
    Transform foodPoint; // Punkt centrum spawnu jedzenia
    void Awake()
    {
        CreateFoodPoint();
        CreateFoodGameObject();
    }

    private void Start()
    {
        // kod na spawnowanie jedzenia

    }

    // spawn jedzenia
    void SpawnFood(Vector2 position)
    {
        Instantiate(food, position, Quaternion.identity);
    }

    // tworzenie GameObjectu jedzenia
    void CreateFoodGameObject()
    {
        food = new GameObject();
        food.SetActive(false);
        food.AddComponent<SpriteRenderer>();
        food.name = "Food";
        food.tag = "Food";
        // dodawanie tekstur do mr�wki
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
