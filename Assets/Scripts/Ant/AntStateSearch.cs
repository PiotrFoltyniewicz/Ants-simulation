using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntStateSearch : AntState
{
    // poruszanie mrowki do przodu
    public override void Move()
    {
        transform.position += transform.up * Ant.movementSpeed * Time.deltaTime;
    }

    // obrot mrowki 
    public override void Turn(float turnAngle)
    {
        float randomTurnAngle = Random.Range(-turnAngle, turnAngle);
        transform.rotation = transform.rotation * Quaternion.Euler(0f, 0f, randomTurnAngle);
    }

}
