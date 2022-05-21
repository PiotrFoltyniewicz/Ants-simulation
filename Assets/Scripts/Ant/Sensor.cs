using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public Ant antScript;
    public int currentState;
    public string pointTag;
    public List<GameObject> insideSensorList = new List<GameObject>();
    public float sensorStrength = 0f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Food" && currentState != 2) FoundFood(collider.transform);
        else if (collider.tag == "Nest" && currentState == 2) FoundNest(collider.transform);
        else if (collider.tag == pointTag)
        {
            if (currentState == 0 && collider.tag == "ToFoodPoint") antScript.ChangeState(1);
            if ((Vector2.Distance(antScript.transform.position, antScript.nest.position) > collider.GetComponent<Point>().distanceToNest && pointTag == "ToNestPoint")
            || (Vector2.Distance(antScript.transform.position, antScript.nest.position) < collider.GetComponent<Point>().distanceToNest && pointTag == "ToFoodPoint"))
                insideSensorList.Add(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        insideSensorList.Remove(collider.gameObject);
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
        foreach (var point in insideSensorList)
        {
            Point pointScript = point.GetComponent<Point>();
            sensorStrength += pointScript.pointStrength;

        }
    }
}
