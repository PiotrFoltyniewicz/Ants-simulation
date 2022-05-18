using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntStateSearch : AntState
{
    // obrot mrowki 
    public override void Turn(float turnAngle)
    {
        float randomTurnAngle = Random.Range(-turnAngle, turnAngle);
        transform.rotation = transform.rotation * Quaternion.Euler(0f, 0f, randomTurnAngle);
    }

    // pozostawienie punktu
    public override void LeavePoint(Vector2 position)
    {
        GameObject temp = Instantiate(pointObject,position, Quaternion.identity, null);
        temp.SetActive(true);
    }

}
