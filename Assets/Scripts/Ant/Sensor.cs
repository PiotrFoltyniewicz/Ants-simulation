using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public Ant antScript;
    public int currentState;
    public string pointTag;
    public List<GameObject> insideSensorList = new List<GameObject>();
    public float sensorStrength;

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        sensorStrength = 0f;
        if(collider.tag == "Food") FoundFood(collider.transform);
        else if (collider.tag == pointTag)
        {
            insideSensorList.Add(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        insideSensorList.Remove(collider.gameObject);
    }

    void FoundFood(Transform food)
    {
        Debug.Log("Znaleziono jedzenie");
        antScript.ChangeState(1);
        antScript.moveTarget = food;
    }

    void CalculateSensorStrength()
    {
        foreach(var point in insideSensorList)
        {
            Point pointScript = point.GetComponent<Point>();
            try
            {
                sensorStrength += pointScript.pointTimeLeft / pointScript.distanceToNest;
            }
            catch
            {
                sensorStrength += pointScript.pointTimeLeft;
            }
        }
    }
}
