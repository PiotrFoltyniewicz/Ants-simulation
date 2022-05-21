using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    protected float pointTime = 20f; // czas jak dlugo punkt sie utrzymuje
    public float pointStrength;
    public float pointTimeLeft; // pozostaly czas do znikniecia
    public float distanceToNest; // odleglosc od mrowiska
    protected float fade; // obliczona wartosc jak mocno o klatke ma malec sila punktu

    protected void SetStartingValues()
    {
        fade = Time.fixedDeltaTime / pointTimeLeft;
    }

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
