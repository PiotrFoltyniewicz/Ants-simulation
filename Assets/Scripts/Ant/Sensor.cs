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
        if (collider.tag == "Food") FoundFood();
        else if (collider.tag == pointTag)
        {
            insideSensorList.Add(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        insideSensorList.Remove(collider.gameObject);
    }

    void FoundFood()
    {
        Debug.Log("Znaleziono jedzenie");
        antScript.ChangeState(1);
    }

    void CalculateSensorStrength()
    {
        foreach (var point in insideSensorList)
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
