using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    GameObject ant; // GameObject mrowki
    public GameObject toFoodPoint; // punkt zostawiajacy mrowka wracajaca z jedzeniem
    public GameObject toNestPoint; // punkt zostawiajacy mrowka szukajaca jedzenia
    private void Awake()
    {
        CreateToFoodPointGameObject();
        CreateToNestPointGameObject();
        CreateAntGameObject();
    }
    void Start()
    {
        // dodac kod aby wszystkie mrowki rozeszly sie w kolku,
        // obliczyc rotacje i dac jako parametr funkcji
        for(int i = 0; i < 100; i++)
        SpawnAnt(0);
    }

    void SpawnAnt(float rotation)
    {
        GameObject temp = Instantiate(ant, transform);
        temp.SetActive(false);
        
        // tutaj obracac pojedyncza mrowke

        temp.SetActive(true);

    }

    void CreateAntGameObject()
    {
        // tworzenie GameObject mrowki i dodawanie komponentow
        ant = new GameObject();
        ant.AddComponent<SpriteRenderer>();
        ant.AddComponent<Ant>();
        //dodawanie komponentow dotyczacych stanow
        ant.AddComponent<AntStateSearch>();
        ant.GetComponent<AntStateSearch>().pointObject = toNestPoint;
        ant.AddComponent<AntStateFollow>();
        ant.GetComponent<AntStateFollow>().pointObject = toNestPoint;
        ant.AddComponent<AntStateReturn>();
        ant.GetComponent<AntStateReturn>().pointObject = toFoodPoint;
        ant.name = "Ant";
        ant.tag = "Ant";
        ant.SetActive(false);
        // dodawanie tekstur do mrowki
        Texture2D antTexture = (Texture2D)Resources.Load("Textures/AntTexture");
        Sprite antSprite = Sprite.Create(antTexture, new Rect(0f, 0f, antTexture.width, antTexture.height), new Vector2(0.5f, 0.5f), 4096);
        ant.GetComponent<SpriteRenderer>().sprite = antSprite;

    }
        void CreateToFoodPointGameObject()
    {
        toFoodPoint = new GameObject();
        toFoodPoint.AddComponent<SpriteRenderer>();
        toFoodPoint.AddComponent<ToFoodPoint>();
        toFoodPoint.name = "ToFoodPoint";
        toFoodPoint.tag = "ToFoodPoint";
        // dodawanie tekstur do mrówki
        Texture2D toFoodPointTexture = (Texture2D)Resources.Load("Textures/Circle");
        Sprite toFoodPointSprite = Sprite.Create(toFoodPointTexture, new Rect(0f, 0f, toFoodPointTexture.width, toFoodPointTexture.height), new Vector2(0.5f, 0.5f), 4096);
        toFoodPoint.GetComponent<SpriteRenderer>().sprite = toFoodPointSprite;
        toFoodPoint.GetComponent<SpriteRenderer>().color = Color.yellow;
        toFoodPoint.SetActive(false);
    }

    void CreateToNestPointGameObject()
    {
        toNestPoint = new GameObject();
        toNestPoint.AddComponent<SpriteRenderer>();
        toNestPoint.AddComponent<ToNestPoint>();
        toNestPoint.name = "ToNestPoint";
        toNestPoint.tag = "ToNestPoint";
        // dodawanie tekstur do mrówki
        Texture2D toNestPointTexture = (Texture2D)Resources.Load("Textures/Circle");
        Sprite toNestPointSprite = Sprite.Create(toNestPointTexture, new Rect(0f, 0f, toNestPointTexture.width, toNestPointTexture.height), new Vector2(0.5f, 0.5f), 4096);
        toNestPoint.GetComponent<SpriteRenderer>().sprite = toNestPointSprite;
        toNestPoint.GetComponent<SpriteRenderer>().color = Color.cyan;
        toNestPoint.SetActive(false);
    }
}
