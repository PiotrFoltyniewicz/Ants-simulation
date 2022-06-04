using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public Ant antScript;
    public int currentState;
    public string pointTag = "ToFoodPoint";
    public float sensorStrength = 0f;
    float sensorRadius = (float)Variables.GetVariable("antSensorRadius");

    float checkTime = 0.15f;
    float checkTimeLeft;
    public List<Transform> insideSensorList = new List<Transform>();

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
    void FoundFood(Transform food)
    {
        antScript.ChangeState(1);
        antScript.moveTarget = food;
        antScript.finalTarget = true;
    }
    void FoundNest(Transform nest)
    {
        antScript.moveTarget = nest;
        antScript.finalTarget = true;
    }

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
