using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * klasa AntState jest abstrakcyjna klasa zawierajaca zmienne i metody wspoldzielone przez klasy dziedziczace odpowiadajace za zachowanie mrowki
 * w zaleznosci od aktualnego stanu
 */
public abstract class AntState : MonoBehaviour
{
    public GameObject pointObject; // GameObject obiektu punktu

    protected float mergePointsRadius = 0.07f; // zasieg laczenia punktow w jeden

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
