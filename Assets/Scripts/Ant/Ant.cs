using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    AntState antState; // zmienna przechowujaca stan mrowki
    public Transform moveTarget = null; // cel nastepnego kroku
    public static float movementSpeed = 0.6f; //maksymalna predkosc
    public static float maxTurnStrength = 0.2f; // maksymalna sila skretu
    public static float stepTime = 0.2f; // czas pomiedzy krokami

    public static float leavePointTime = 0.5f;

    public GameObject[] sensors;

    float leavePointTimeLeft = 0f;
    float stepTimeLeft = 0f; // czas pozostaly do nastepnego kroku
    int currentState; // obecny stan w postaci cyfry
    AntState[] states;

    void Awake()
    {
        // dodanie komponentow stanow mrowki do obiektu
        states = new AntState[3] { gameObject.GetComponent<AntStateSearch>(), gameObject.GetComponent<AntStateFollow>(), gameObject.GetComponent<AntStateReturn>() };
    }

    void Start()
    {
        ChangeState(0);
    }

    void Update()
    {
        // jezeli nadszedl czas zrobienia kroku to obrot mrowki i ruch
        stepTimeLeft -= Time.deltaTime;
        leavePointTimeLeft -= Time.deltaTime;
        if (leavePointTimeLeft < 0)
        {
            leavePointTimeLeft = leavePointTime;
            antState.LeavePoint(transform.position);
        }
        if (stepTimeLeft < 0)
        {
            stepTimeLeft = stepTime;
            antState.Turn(maxTurnStrength, moveTarget);
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
        foreach(var sensor in sensors)
        {
            Sensor sensorScript = sensor.GetComponent<Sensor>();
            sensorScript.currentState = currentState;
            sensorScript.insideSensorList.Clear();
            sensorScript.sensorStrength = 0f;
            if(currentState == 0 || currentState == 1) sensorScript.pointTag = "ToFoodPoint";
            else sensorScript.pointTag = "ToNestPoint";
        }
    }

    private void OnCollisionEnter2D(Collision2D coll) 
    {
        // jezeli dotknie jedzenia, ale nie trzyma jedzenia
        if(coll.gameObject.tag == "Food" && currentState != 1)
        {
            coll.gameObject.GetComponent<Food>().TakeFood();
            ChangeState(2);
            transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.z + 180f);
            moveTarget = null;
        }
        float bounce = transform.eulerAngles.z;
        //odbicie mrowki od sciany
        transform.rotation = Quaternion.Euler(0, 0, bounce + Random.Range(150, 210));
    }
}
