using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    GameObject ant; // GameObject mrowki
    public GameObject toFoodPoint; // punkt zostawiajacy mrowka wracajaca z jedzeniem
    public GameObject toNestPoint; // punkt zostawiajacy mrowka szukajaca jedzenia
    public static List<Transform> toNestList = new List<Transform>();
    public static List<Transform> toFoodList = new List<Transform>();
    int antNumber = 100; //liczba mrówek
    private void Awake()
    {
        CreateToFoodPointGameObject();
        CreateToNestPointGameObject();
        CreateAntGameObject();
    }
    void Start()
    {
        for(int i = 0; i < antNumber; i++)
        {
            ant.transform.eulerAngles = new Vector3(
            ant.transform.eulerAngles.x,
            ant.transform.eulerAngles.y,
            ant.transform.eulerAngles.z + 360 / (float)antNumber);
        SpawnAnt();
        }
    }

    void SpawnAnt()
    {
        Instantiate(ant, transform).SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ant")
        {
            collider.GetComponent<Ant>().RestorePoints();
            if (collider.GetComponent<Ant>().currentState != 2) return;
            collider.GetComponent<Ant>().TouchedNest();
        }
    }

    void CreateAntGameObject()
    {
        // tworzenie GameObject mrowki i inne
        ant = new GameObject();
        ant.AddComponent<SpriteRenderer>();
        ant.AddComponent<Ant>();
        ant.AddComponent<CircleCollider2D>();
        ant.GetComponent<CircleCollider2D>().radius = 0.05f;
        ant.AddComponent<Rigidbody2D>();
        ant.GetComponent<Rigidbody2D>().gravityScale = 0f;
        ant.layer = 6;

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

        // dodawanie i konfiguracja czujnikow
        GameObject leftSensor = new GameObject("LeftSensor", typeof(Sensor));
        GameObject middleSensor = new GameObject("MiddleSensor", typeof(Sensor));
        GameObject rightSensor = new GameObject("RightSensor", typeof(Sensor));
        GameObject[] sensors = new GameObject[3]{leftSensor, middleSensor, rightSensor};
        leftSensor.transform.parent = ant.transform;
        middleSensor.transform.parent = ant.transform;
        rightSensor.transform.parent = ant.transform;

        foreach (var sensor in sensors)
        {
            sensor.GetComponent<Sensor>().antScript = ant.GetComponent<Ant>();
            sensor.transform.position += new Vector3(0f, 0.4f, 0f);
        }
        sensors[0].transform.position += new Vector3(-0.2f, -0.15f, 0f);
        sensors[2].transform.position += new Vector3(0.2f, -0.15f, 0f);

        ant.GetComponent<Ant>().sensors = sensors;
        ant.GetComponent<Ant>().nest = transform;
    }
    void CreateToFoodPointGameObject()
    {
        toFoodPoint = new GameObject();
        toFoodPoint.AddComponent<SpriteRenderer>();
        toFoodPoint.AddComponent<ToFoodPoint>();
        toFoodPoint.name = "ToFoodPoint";
        toFoodPoint.tag = "ToFoodPoint";
        toFoodPoint.layer = 7;
        // dodawanie tekstur do mrówki
        Texture2D toFoodPointTexture = (Texture2D)Resources.Load("Textures/Circle");
        Sprite toFoodPointSprite = Sprite.Create(toFoodPointTexture, new Rect(0f, 0f, toFoodPointTexture.width, toFoodPointTexture.height), new Vector2(0.5f, 0.5f), 8192);
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
        toFoodPoint.layer = 7;
        // dodawanie tekstur do mrówki
        Texture2D toNestPointTexture = (Texture2D)Resources.Load("Textures/Circle");
        Sprite toNestPointSprite = Sprite.Create(toNestPointTexture, new Rect(0f, 0f, toNestPointTexture.width, toNestPointTexture.height), new Vector2(0.5f, 0.5f), 8192);
        toNestPoint.GetComponent<SpriteRenderer>().sprite = toNestPointSprite;
        toNestPoint.GetComponent<SpriteRenderer>().color = Color.cyan;
        toNestPoint.SetActive(false);
    }
}
