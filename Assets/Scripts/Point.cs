using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    protected float pointTime = 30f; // czas jak dlugo punkt sie utrzymuje
    public float pointStrength;
    public float pointTimeLeft; // pozostaly czas do znikniecia
    public float distanceToSource; // odleglosc od mrowiska
    protected float fade; // obliczona wartosc jak mocno o klatke ma malec sila punktu
    public Transform source;

    protected void SetStartingValues()
    {
        fade = Time.fixedDeltaTime / pointTimeLeft;
    }

    protected virtual float FindDistanceToSource(Transform source)
    {
        return Vector2.Distance(transform.position, source.position);
    }

    // funkcja odpowiadajaca za usuniecie punktu
    protected void Disappear()
    {
        Destroy(gameObject);
    }
}
