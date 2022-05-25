using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public Ant antScript;
    public int currentState;
    public string pointTag = "ToFoodPoint";
    public float sensorStrength = 0f;
    float sensorRadius = 0.1f;

    float checkTime = 0.15f;
    float checkTimeLeft;
    public List<Transform> insideSensorList = new List<Transform>();

    void Start()
    {
        checkTimeLeft = checkTime;
    }
    void FixedUpdate()
    {
        checkTimeLeft -= Time.deltaTime;
        if(checkTimeLeft <= 0)
        {
            Check();
            checkTimeLeft = checkTime;
        }
    }
    void Check()
    {
        if(Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Food").transform.position) <= sensorRadius && currentState != 2)
        {
            FoundFood(transform);
        }
        else if(Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Nest").transform.position) <= sensorRadius && currentState == 2)
        {
            FoundNest(transform);
        }
        else if(pointTag == "toFoodPoint")
            foreach(var point in Nest.toFoodList)
            {
                if(Vector2.Distance(transform.position, point.position) <= sensorRadius)
                {
                    if(currentState == 0)
                    {
                        antScript.ChangeState(1);
                        antScript.moveTarget = point.transform;
                        return;
                    }
                    else if(Vector2.Distance(antScript.transform.position, antScript.nest.position) < Vector2.Distance(point.transform.position, antScript.nest.position))
                    {
                        insideSensorList.Add(point);
                    }
                }
                else if (insideSensorList.Contains(point))
                {
                    insideSensorList.Remove(point);
                }
            }
        else if (pointTag == "toNestPoint")
        {
            foreach(var point in Nest.toNestList)
            {
                if(Vector2.Distance(transform.position, point.position) <= sensorRadius && Vector2.Distance(antScript.transform.position, antScript.nest.position) > Vector2.Distance(point.transform.position, antScript.nest.position))
                {
                    insideSensorList.Add(point);
                }
                else if (insideSensorList.Contains(point))
                {
                    insideSensorList.Remove(point);
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
            foreach(var point in insideSensorList)
            {
                if(point != null && Vector2.Distance(transform.position, point.position) <= sensorRadius)
                {
                    Point pointScript = point.GetComponent<Point>();
                    sensorStrength += pointScript.pointStrength;
                }
            }
    }
}
