using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public Ant antScript;
    public int currentState;
    public string pointTag = "ToFoodPoint";
    public List<GameObject> insideSensorList = new List<GameObject>();
    public float sensorStrength = 0f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Food" && currentState != 2) FoundFood(collider.transform);
        else if (collider.tag == "Nest" && currentState == 2) FoundNest(collider.transform);
        if (collider.tag == pointTag)
        {
            if (currentState == 0 && collider.tag == "ToFoodPoint") antScript.ChangeState(1);

            if (currentState == 1 && Vector2.Distance(transform.position, antScript.nest.position) > Vector2.Distance(collider.transform.position, GameObject.Find("Nest").transform.position))
            {
                antScript.moveTarget = collider.transform;
                return;
            }
            if (currentState == 2 && Vector2.Distance(transform.position, antScript.nest.position) < Vector2.Distance(collider.transform.position, GameObject.Find("Nest").transform.position)) return;
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
