using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    Rigidbody2D rb; // komponent odpowiadajacy za fizyke obiektu
    public static float movementSpeed = 1.5f; //maksymalna predkosc
    public static float maxTurnAngle = 15f; // maksymalny kat skretu
    public static float stepTime = 0.5f; // czas pomiedzy krokami
    public float stepTimeLeft = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<AntState>().antScript = this;
    }

    void Start()
    {
        //powiekszenie na cele testow
        transform.localScale *= 8f;
    }

    void Update()
    {
        stepTimeLeft -= Time.deltaTime;

        if(stepTimeLeft <= 0f)
        {
            stepTimeLeft = stepTime;
            
        }
    }
}
