using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * klasa abstrakcyjna Point posiada zmienne i metody wspoldzielone przez klasy dziedziczace, odpowiada za dzialanie punktow
 */
public abstract class Point : MonoBehaviour
{
    protected float pointTime = 20f; // czas jak dlugo punkt sie utrzymuje
    public float pointStrength; // sila punktu
    public float pointTimeLeft; // pozostaly czas do znikniecia
    public float distanceToSource; // odleglosc od zrodla
    protected float fade; // obliczona wartosc jak mocno co klatke ma znikac punkt
    public Transform source; // pozycja zrodla
    protected float scale; // wartosc jak mocno co klatke ma malec sila punktu
    protected SpriteRenderer spriteRenderer; // zmienna przechowujaca komponent punktu odpowiadajacy za jego wyswietlanie

    // ustawienie poczatkawych wartosci
    protected void SetStartingValues()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
    }

    // ustawienie wartosci w momencie pojawienia sie punktu
    public void OnCreate()
    {
        distanceToSource = FindDistanceToSource(source);
        if (distanceToSource < 0.5f) pointTimeLeft = 2 * pointTime;
        else pointTimeLeft = pointTime;
        SetStartingValues();
        pointStrength = 1 / distanceToSource;
        scale = pointStrength * (Time.deltaTime / pointTimeLeft);
    }
    // obliczenie odleglosci od punktu do zrodla
    protected virtual float FindDistanceToSource(Transform source)
    {
        return Vector2.Distance(transform.position, source.position);
    }

    // metoda odpowiadajaca za znikanie punktu i wyzerowanie wartosci
    protected void Dissapear()
    {
        gameObject.SetActive(false);
        source = null;
        pointStrength = 0;
        pointTimeLeft = 0;
        distanceToSource = 0;
        fade = 0;
        scale = 0;

    }
}
