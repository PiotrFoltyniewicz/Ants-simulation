using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntStateSearch : AntState
{
    public override void Move()
    {
        transform.position += transform.up * Ant.movementSpeed * Time.deltaTime;
    }

    // mrowka idzie losowo przed siebie
    public override void Turn()
    {
        float randomTurnAngle = Random.Range(-Ant.turnStrength, Ant.turnStrength);
        transform.rotation = Quaternion.identity * Quaternion.Euler(0f, 0f, randomTurnAngle);
    }
}
