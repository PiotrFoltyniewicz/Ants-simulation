using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    GameObject nest; // zmienna przechowujaca obiekt mrowiska
    private void Awake()
    {
        Time.timeScale = 2f;
        gameObject.AddComponent<FoodManager>();
        CreateNestGameObject();
        CreateBoundaryColliders();
    }

    // tworzenie GameObjectu mrowiska
    void CreateNestGameObject()
    {
        nest = new GameObject();
        nest.AddComponent<SpriteRenderer>();
        nest.AddComponent<Nest>();
        nest.AddComponent<CircleCollider2D>().isTrigger = true;
        nest.GetComponent<CircleCollider2D>().radius = 0.5f;
        nest.name = "Nest";
        nest.tag = "Nest";
        // dodawanie tekstur do mrówki
        Texture2D nestTexture = (Texture2D)Resources.Load("Textures/Circle");
        Sprite nestSprite = Sprite.Create(nestTexture, new Rect(0f, 0f, nestTexture.width, nestTexture.height), new Vector2(0.5f, 0.5f), 256);
        nest.GetComponent<SpriteRenderer>().sprite = nestSprite;
        nest.GetComponent<SpriteRenderer>().color = Color.blue;
        // pozycjonowanie gniazda w losowym miejscu
        nest.transform.position = new Vector2(Random.Range(-8, 8), Random.Range(-4, 4));

    }
    void CreateBoundaryColliders()
    {
        BoxCollider2D top = gameObject.AddComponent<BoxCollider2D>();
        top.size = new Vector2(19f, 1f);
        top.offset = new Vector2(0f, 5.5f);
        BoxCollider2D bottom = gameObject.AddComponent<BoxCollider2D>();
        bottom.size = new Vector2(19f, 1f);
        bottom.offset = new Vector2(0f, -5.5f);
        BoxCollider2D right = gameObject.AddComponent<BoxCollider2D>();
        right.size = new Vector2(1f, 11f);
        right.offset = new Vector2(9.5f, 0f);
        BoxCollider2D left = gameObject.AddComponent<BoxCollider2D>();
        left.size = new Vector2(1f, 11f);
        left.offset = new Vector2(-9.5f, 0f);
    }
}
