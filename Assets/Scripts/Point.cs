using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Point : MonoBehaviour
{
    protected float pointTime = 25f; // czas jak dlugo punkt sie utrzymuje
    public float pointStrength;
    public float pointTimeLeft; // pozostaly czas do znikniecia
    public float distanceToSource; // odleglosc od mrowiska
    protected float fade; // obliczona wartosc jak mocno o klatke ma malec sila punktu
    public Transform source;
    protected float scale;
    protected SpriteRenderer spriteRenderer;

    protected void SetStartingValues()
    {
        fade = Time.fixedDeltaTime / pointTimeLeft;
        scale = fade;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
    }
    public void OnCreate()
    {
        distanceToSource = FindDistanceToSource(source);
        pointTimeLeft = pointTime;
        pointStrength = 1 / distanceToSource;
        scale *= pointStrength;
        SetStartingValues();
    }

    protected virtual float FindDistanceToSource(Transform source)
    {
        return Vector2.Distance(transform.position, source.position);
    }

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
