using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AntState : MonoBehaviour
{
    public GameObject pointObject; // GameObject obiektu punktu

    // funkcja odpowiadajaca za ruch
    public void Move()
    {
        transform.position += transform.up * Ant.movementSpeed * Time.deltaTime;

    }

    // funkcja ktora ma implementacje w klasach dziedziczacych, odpowiada za obrot mrowki
    public abstract void Turn(float turnAngle, Transform target);

    // funkcja ktora ma implementacje w klasach dziedziczacych, odpowiada za pozostawienie punktu
    public abstract void LeavePoint(Vector2 position, Transform source);

}
