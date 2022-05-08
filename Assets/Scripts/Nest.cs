using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    GameObject ant;
    private void Awake()
    {
        CreateAntGameObject();
    }
    void Start()
    {
        //dopisalem essa
        for(int i = 0; i < 100; i++)
        SpawnAnt();
    }

    void SpawnAnt()
    {
        GameObject temp = Instantiate(ant, transform);
        // dodac kod obracajacy mrowke aby wszystkie mrowki rozeszly sie w kolku


        temp.SetActive(true);

    }

    void CreateAntGameObject()
    {
        // tworzenie GameObject mrowki i dodawanie komponentow
        ant = new GameObject();
        ant.AddComponent<SpriteRenderer>();
        ant.AddComponent<Ant>();
        ant.name = "Ant";
        ant.SetActive(false);
        // dodawanie tekstur do mrowki
        Texture2D antTexture = (Texture2D)Resources.Load("Textures/AntTexture");
        Sprite antSprite = Sprite.Create(antTexture, new Rect(0f, 0f, antTexture.width, antTexture.height), new Vector2(0.5f, 0.5f), 4096);
        ant.GetComponent<SpriteRenderer>().sprite = antSprite;

    }
}
