using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    AntState antState; // zmienna przechowujaca stan mrowki
    public static float movementSpeed = 1f; //maksymalna predkosc
    public static float maxTurnAngle = 10f; // maksymalna sila skretu
    public static float stepTime = 0.2f; // czas pomiedzy krokami

    public static float leavePointTime = 0.5f;

    float leavePointTimeLeft = 0f;
    float stepTimeLeft = 0f; // czas pozostaly do nastepnego kroku
    int currentState; // obecny stan w postaci cyfry
    AntState[] states;
     
    void Awake()
    {
        // dodanie komponentow stanow mrowki do obiektu
        states = new AntState[3]{ gameObject.GetComponent<AntStateSearch>(), gameObject.GetComponent<AntStateFollow>(), gameObject.GetComponent<AntStateReturn>() };
    }

    void Start()
    {
        //powiekszenie na cele testow
        //transform.localScale *= 4f;
        

        ChangeState(0);
    }

    void Update()
    {
        // jezeli nadszedl czas zrobienia kroku to obrot mrowki i ruch
        stepTimeLeft -= Time.deltaTime;
        leavePointTimeLeft -= Time.deltaTime;
        if(leavePointTimeLeft < 0 )
        {
            leavePointTimeLeft = leavePointTime;
            antState.LeavePoint(transform.position);
        }
        if(stepTimeLeft < 0)
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
                currentState = 0;
                break;
            // stan podazania za sladami do jedzenia
            case 1:
                antState = states[1];
                currentState = 1;
                break;
            // stan wracania z jedzeniem do mrowiska
            case 2:
                antState = states[2];
                currentState = 2;
                break;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D coll) {
        float bounce = transform.eulerAngles.z;
        //odbicie mrowki od sciany
        transform.rotation = Quaternion.Euler(0, 0, bounce + Random.Range(150, 210));
    }
}
