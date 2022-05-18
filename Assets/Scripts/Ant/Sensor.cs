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

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        
    }
}
