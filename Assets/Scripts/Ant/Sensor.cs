using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * klasa Sensor odpowiada za dzialanie czujnika, obliczanie jego sily i sprawdzanie jakie punkty na mapie sa w jego zasiegu
 */
public class Sensor : MonoBehaviour
{
    public Ant antScript; // zmienna przechowujaca skrypt mrowki do ktorej nalezy ten czujnik
    public int currentState; // aktualny stan mrowki
    public string pointTag = "ToFoodPoint"; // tag punktu, jakiego szuka mrowka
    public float sensorStrength = 0f; // aktualna sila czujnika
    float sensorRadius = (float)Variables.GetVariable("antSensorRadius"); // promien czujnika

    float checkTime = 0.15f; // czas pomiedzy szukaniem przez czujnik punktow znajdujacych sie w nim
    float checkTimeLeft; // czas pozostaly pomiedzy szukaniem przez czujnik punktow znajdujacych sie w nim
    public List<Transform> insideSensorList = new List<Transform>(); // lista punktow znajdujacych sie w czujnku

    void Start()
    {
        checkTimeLeft = checkTime;
    }
    void Update()
    {
        checkTimeLeft -= Time.deltaTime;
        if (checkTimeLeft <= 0)
        {
            checkTimeLeft = checkTime;
            Check();
        }
    }
    // metoda sprawdzajaca jakie punkty znajduja sie w czujniku
    void Check()
    {
        insideSensorList.Clear();
        foreach (Transform food in FoodManager.foodList)
        {
            if (Vector2.Distance(transform.position, food.transform.position) <= sensorRadius && currentState != 2)
            {
                FoundFood(transform);
                return;
            }
        }
        if (Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Nest").transform.position) <= sensorRadius && currentState == 2)
        {
            FoundNest(transform);
            return;
        }
        if (pointTag == "ToFoodPoint")
        {
            foreach (GameObject point in ObjectPooling.pooledToFoodPoints)
            {
                if (!point.activeInHierarchy) continue;
                if (Vector2.Distance(transform.position, point.transform.position) <= sensorRadius)
                {
                    if (currentState == 0)
                    {
                        antScript.ChangeState(1);
                        antScript.moveTarget = point.transform;
                        return;
                    }
                    else if (Vector2.Distance(antScript.transform.position, antScript.nest.position) < Vector2.Distance(point.transform.position, antScript.nest.position))
                    {
                        insideSensorList.Add(point.transform);
                    }
                }
            }
        }
        else if (pointTag == "ToNestPoint")
        {
            foreach (GameObject point in ObjectPooling.pooledToNestPoints)
            {
                if (Vector2.Distance(transform.position, point.transform.position) <= sensorRadius && Vector2.Distance(antScript.transform.position, antScript.nest.position) > Vector2.Distance(point.transform.position, antScript.nest.position))
                {
                    insideSensorList.Add(point.transform);
                }
            }
        }
    }

    // metoda odpowiadajaca za zachowanie mrowki w momencie 'zobaczenia' jedzenia
    void FoundFood(Transform food)
    {
        antScript.ChangeState(1);
        antScript.moveTarget = food;
        antScript.finalTarget = true;
    }
    // metoda odpowiadajaca za zachowanie mrowki w momencie 'zobaczenia' mrowiska
    void FoundNest(Transform nest)
    {
        antScript.moveTarget = nest;
        antScript.finalTarget = true;
    }
    // metoda obliczajaca sile czujnika
    public void CalculateSensorStrength()
    {
        sensorStrength = 0f;
        foreach (Transform point in insideSensorList)
        {
            if (point.gameObject.activeInHierarchy && Vector2.Distance(transform.position, point.position) <= sensorRadius)
            {
                Point pointScript = point.GetComponent<Point>();
                sensorStrength += pointScript.pointStrength;
            }
        }
    }
}
