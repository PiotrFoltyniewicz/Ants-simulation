using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    protected float pointTime = 10f; // czas jak dlugo punkt sie utrzymuje
    protected float pointTimeLeft; // pozostaly czas do znikniecia
    public float distanceToNest; // odleglosc od mrowiska
    public float strength; // sila jaki mocny jest punkt (maleje z czasem)

    protected virtual float FindDistanceToNest()
    {
        return Vector2.Distance(transform.position, GameObject.Find("Nest").transform.position);
    }

    // funkcja odpowiadajaca za usuniecie punktu
    protected void Disappear()
    {
        Destroy(gameObject);
    }
}
