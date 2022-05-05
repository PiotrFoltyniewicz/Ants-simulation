using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // tymczasowy obiekt gry, potem bêdzie znajdowa³ siê w skrypcie dla mrowiska
    GameObject ant;
    private void Awake()
    {
    }
    void Start()
    {
        CreateAntGameObject();
        SpawnAnt();
    }
    //tymczasowa funkcja do tworzenia mrówki, bêdzie siê znajdowa³a w kodzie mrowiska
    void CreateAntGameObject()
    {
        ant = new GameObject("Ant", typeof(SpriteRenderer), typeof(Ant));
        ant.SetActive(false);
        Texture2D antTexture = (Texture2D)Resources.Load("Textures/AntTexture");
        Sprite antSprite;
        antSprite = Sprite.Create(antTexture, new Rect(0f, 0f, antTexture.width, antTexture.height), new Vector2(0.5f, 0.5f), 4096);
        ant.GetComponent<SpriteRenderer>().sprite = antSprite;
    }
    // tymczasowa funkcja
    void SpawnAnt()
    {
        GameObject temp = Instantiate(ant);
        temp.SetActive(true);
    }

}
