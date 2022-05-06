using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // tymczasowy obiekt gry, potem bedzie znajdowal sie w skrypcie dla mrowiska

    GameObject nest;
    private void Awake()
    {
    }
    void Start()
    {
        CreateNestGameObject();
    }

    void CreateNestGameObject()
    {
        nest = new GameObject();
        nest.AddComponent<SpriteRenderer>();
        nest.AddComponent<Nest>();
        nest.name = "Nest";
        // dodawanie tekstur do mrówki
        Texture2D nestTexture = (Texture2D)Resources.Load("Textures/Circle");
        Sprite nestSprite = Sprite.Create(nestTexture, new Rect(0f, 0f, nestTexture.width, nestTexture.height), new Vector2(0.5f, 0.5f), 256);
        nest.GetComponent<SpriteRenderer>().sprite = nestSprite;
        nest.GetComponent<SpriteRenderer>().color = Color.blue;
        nest.transform.position = new Vector2(Random.Range(-8,8), Random.Range(-4,4));

    }  

}
