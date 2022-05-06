using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // tymczasowy obiekt gry, potem bedzie znajdowal sie w skrypcie dla mrowiska
    GameObject ant;
    private void Awake()
    {
    }
    void Start()
    {
        CreateAntGameObject();
        SpawnAnt();
    }
    //tymczasowa funkcja do tworzenia mrowki, bedzie sie znajdowala w kodzie mrowiska
    void CreateAntGameObject()
    {
        // tworzenie GameObject mrowki i dodawanie komponentow
        ant = new GameObject();
        ant.AddComponent<SpriteRenderer>();
        ant.AddComponent<Rigidbody2D>();
        ant.AddComponent<Ant>();
        ant.AddComponent<AntState>();
        ant.AddComponent<AntStateSearch>();
        ant.AddComponent<AntStateFollow>();
        ant.AddComponent<AntStateReturn>();
        ant.name = "Ant";
        ant.SetActive(false);
        // dodawanie tekstur do mrówki
        Texture2D antTexture = (Texture2D)Resources.Load("Textures/AntTexture");
        Sprite antSprite = Sprite.Create(antTexture, new Rect(0f, 0f, antTexture.width, antTexture.height), new Vector2(0.5f, 0.5f), 4096);
        ant.GetComponent<SpriteRenderer>().sprite = antSprite;
        // zmiany w komponencie Rigidbody (fizyka mrówki)
        ant.GetComponent<Rigidbody2D>().gravityScale = 0f;

    }
    // tymczasowa funkcja
    void SpawnAnt()
    {
        GameObject temp = Instantiate(ant);
        temp.SetActive(true);
    }

}
