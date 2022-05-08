using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    AntState antState; // zmienna przechowujaca stan mrowki
    public static float movementSpeed = 4f; //maksymalna predkosc
    public static float maxTurnAngle = 10f; // maksymalna sila skretu
    public static float stepTime = 0.2f; // czas pomiedzy krokami
    float stepTimeLeft = 0f; // czas pozostaly do nastepnego kroku

    AntState[] states;

    void Awake()
    {
        // dodanie komponentow stanow mrowki do obiektu
        states = new AntState[3]{ gameObject.AddComponent<AntStateSearch>(), gameObject.AddComponent<AntStateFollow>(), gameObject.AddComponent<AntStateReturn>() };
    }

    void Start()
    {
        //powiekszenie na cele testow
        transform.localScale *= 8f;
        

        ChangeState(0);
    }

    void Update()
    {
        // jezeli nadszedl czas zrobienia kroku to obrot mrowki i ruch
        stepTimeLeft -= Time.deltaTime;
        if(stepTimeLeft <= 0)
        {
            stepTimeLeft = stepTime;
            antState.Turn(maxTurnAngle);
        }

        antState.Move();
    }

    // zmiana stanu mrowki
    public void ChangeState(int stateNum)
    {
        switch (stateNum)
        {
            // stan szukania sladow/jedzenia
            case 0:
                antState = states[0];
                break;
            // stan podazania za sladami do jedzenia
            case 1:
                antState = states[1];
                break;
            // stan wracania z jedzeniem do mrowiska
            case 2:
                antState = states[2];
                break;
        }
    }
}
