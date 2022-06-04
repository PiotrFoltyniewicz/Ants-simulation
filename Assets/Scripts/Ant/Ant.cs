using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * klasa Ant zawiera zmienne i metody odpowiadajace za podstawowe zachowanie mrowki, zmiane stanow oraz wywolywanie bardziej specyficznych
 * metod znajdujacych sie w innych klasach
*/
public class Ant : MonoBehaviour
{
    AntState antState; // zmienna przechowujaca stan mrowki
    public Transform moveTarget = null; // cel nastepnego kroku
    public static float movementSpeed = (float)Variables.GetVariable("antMoveSpeed"); // maksymalna predkosc
    float maxTurnStrength = (float)Variables.GetVariable("antMaxTurnStrength"); // maksymalna sila skretu
    float stepTime = (float)Variables.GetVariable("antStepTime"); // czas pomiedzy krokami
    float leavePointTime = (float)Variables.GetVariable("antLeavePointTime"); // czas pomiedzy zostawianiem punktow
    int maxNumOfPoints = (int)Variables.GetVariable("antMaxNumberOfPoints"); // maksymalna ilosc punktow 
    public bool finalTarget = false; // wartosc staje sie true kiedy mrowka 'widzi' jedzenie lub mrowisko
    int remainingPoints; // pozostale punkty do zostawienia
    public GameObject[] sensors; // tablica przechowujaca GameObjecty czujnikow
    float leavePointTimeLeft = 0f; // czas pozostaly do polozenia punktu
    float stepTimeLeft = 0f; // czas pozostaly do nastepnego kroku
    public int currentState; // obecny stan w postaci cyfry
    AntState[] states; // tablica przechowujaca klasy odpowiadajace za zachowanie mrówki wed³ug danego stanu
    public Transform nest; // zmienna przechowujaca pozycje mrowiska
    public Transform pickedFoodPosition; // zmienna przechowujaca pozycje ostatnio podniesionego jedzenia

    void Awake()
    {
        states = new AntState[3] { gameObject.GetComponent<AntStateSearch>(), gameObject.GetComponent<AntStateFollow>(), gameObject.GetComponent<AntStateReturn>() };
    }

    void Start()
    {
        remainingPoints = maxNumOfPoints;
        ChangeState(0);
    }

    void Update()
    {
        stepTimeLeft -= Time.deltaTime;
        leavePointTimeLeft -= Time.deltaTime;
        // pozostawienie punktu
        if (leavePointTimeLeft < 0 && remainingPoints > 0)
        {
            leavePointTimeLeft = leavePointTime;
            if (currentState == 2)
            {
                antState.LeavePoint(transform.position, pickedFoodPosition);
            }
            else
            {
                antState.LeavePoint(transform.position, nest);
            }
            remainingPoints--;
        }
        // wykonanie obrotu
        if (stepTimeLeft < 0)
        {
            stepTimeLeft = stepTime;
            if (finalTarget)
            {
                antState.Turn(0f, moveTarget);
                CheckSensors();
            }
            else
            {
                moveTarget = CheckSensors();
                if (Vector2.Distance(transform.position, nest.position) <= 1 && currentState == 2) moveTarget = nest;
                antState.Turn(maxTurnStrength, moveTarget);
            }
        }
        // wykonanie ruchu
        antState.Move();
    }
    // zmiana stanu mrowki
    public void ChangeState(int stateNum)
    {
        if (currentState != stateNum)
            foreach (var sensor in sensors)
            {
                Sensor sensorScript = sensor.GetComponent<Sensor>();
                sensorScript.currentState = stateNum;
                sensorScript.insideSensorList.Clear();
                sensorScript.sensorStrength = 0f;
                if (stateNum == 0 || stateNum == 1) sensorScript.pointTag = "ToFoodPoint";
                else sensorScript.pointTag = "ToNestPoint";
            }
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
    // obliczenie sily kazdego z czujnikow i zwrocenie pozycji najsilniejszego czujnika
    Transform CheckSensors()
    {
        finalTarget = false;
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
        return chosen;
    }
    // zmiana stanu mrowki i obrot w przypadku dotkniecia jedzenia
    public void TouchedFood(Transform foodPos)
    {
        ChangeState(2);
        pickedFoodPosition = foodPos;
        RestorePoints();
        float angle = Mathf.Atan2(nest.position.y - transform.position.y, nest.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        moveTarget = null;
        finalTarget = false;
    }
    // zmiana stanu mrowki i obrot w przypadku dotkniecia mrowiska
    public void TouchedNest()
    {
        ChangeState(0);
        RestorePoints();
        if (pickedFoodPosition != null)
        {
            float angle = Mathf.Atan2(pickedFoodPosition.position.y - transform.position.y, pickedFoodPosition.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + 180f);
        }
        moveTarget = null;
        finalTarget = false;
    }
    // odnowienie liczby pozostalych punktow
    public void RestorePoints()
    {
        remainingPoints = maxNumOfPoints;
    }
    // odbicie mrowki od sciany
    private void OnCollisionEnter2D(Collision2D coll)
    {
        float bounce = transform.eulerAngles.z;
        transform.rotation = Quaternion.Euler(0, 0, bounce + Random.Range(150, 210));
    }
}
