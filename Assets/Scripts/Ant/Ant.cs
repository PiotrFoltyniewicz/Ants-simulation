using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    AntState antState; // zmienna przechowujaca stan mrowki
    public Transform moveTarget = null; // cel nastepnego kroku
    public Transform prevPosition;
    public static float movementSpeed = 1f; //maksymalna predkosc
    public static float maxTurnStrength = 0.2f; // maksymalna sila skretu
    public static float stepTime = 0.2f; // czas pomiedzy krokami
    public static float leavePointTime = 0.3f;
    public static int maxNumOfPoints = 50;
    public bool finalTarget = false;
    int remainingPoints;
    public GameObject[] sensors;
    float leavePointTimeLeft = 0f;
    float stepTimeLeft = 0f; // czas pozostaly do nastepnego kroku
    public int currentState; // obecny stan w postaci cyfry
    AntState[] states;
    public Transform nest;

    void Awake()
    {
        // dodanie komponentow stanow mrowki do obiektu
        states = new AntState[3] { gameObject.GetComponent<AntStateSearch>(), gameObject.GetComponent<AntStateFollow>(), gameObject.GetComponent<AntStateReturn>() };
    }

    void Start()
    {
        remainingPoints = maxNumOfPoints;
        ChangeState(0);
    }

    void Update()
    {
        // jezeli nadszedl czas zrobienia kroku to obrot mrowki i ruch
        stepTimeLeft -= Time.deltaTime;
        leavePointTimeLeft -= Time.deltaTime;
        if (leavePointTimeLeft < 0 && remainingPoints > 0)
        {
            leavePointTimeLeft = leavePointTime;
            antState.LeavePoint(transform.position);
            remainingPoints--;
        }
        if (stepTimeLeft < 0)
        {
            stepTimeLeft = stepTime;
            if (finalTarget) antState.Turn(0f, moveTarget);
            else
            {
                moveTarget = CheckSensors();
                antState.Turn(maxTurnStrength, moveTarget);
            }
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
                remainingPoints = maxNumOfPoints;
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
                remainingPoints = maxNumOfPoints;
                antState = states[2];
                currentState = 2;
                break;
        }
        foreach (var sensor in sensors)
        {
            Sensor sensorScript = sensor.GetComponent<Sensor>();
            sensorScript.currentState = currentState;
            sensorScript.insideSensorList.Clear();
            sensorScript.sensorStrength = 0f;
            if (currentState == 0 || currentState == 1) sensorScript.pointTag = "ToFoodPoint";
            else sensorScript.pointTag = "ToNestPoint";
        }
    }
    Transform CheckSensors()
    {
        foreach (var sensor in sensors)
        {
            sensor.GetComponent<Sensor>().CalculateSensorStrength();
        }
        Transform chosen = sensors[1].transform;
        foreach (var sensor in sensors)
        {
            Sensor sensorScript = sensor.GetComponent<Sensor>();
            if (sensorScript.sensorStrength > chosen.GetComponent<Sensor>().sensorStrength) chosen = sensor.transform;
        }
        //  Debug.Log(chosen.name);
        // Debug.Log(sensors[0].name + "  " + sensors[0].GetComponent<Sensor>().sensorStrength);
        // Debug.Log(sensors[1].name + "  " + sensors[1].GetComponent<Sensor>().sensorStrength);
        // Debug.Log(sensors[2].name + "  " + sensors[2].GetComponent<Sensor>().sensorStrength);
        return chosen;
    }
    public void TouchedFood()
    {
        ChangeState(2);
        float angle = Mathf.Atan2(nest.position.y - transform.position.y, nest.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        moveTarget = null;
        finalTarget = false;
    }

    public void TouchedNest()
    {
        ChangeState(1);
        transform.rotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + 180f);
        moveTarget = null;
        finalTarget = false;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        float bounce = transform.eulerAngles.z;
        //odbicie mrowki od sciany
        transform.rotation = Quaternion.Euler(0, 0, bounce + Random.Range(150, 210));
    }
}
