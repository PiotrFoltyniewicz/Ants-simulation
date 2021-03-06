using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * klasa ToFoodPoint dziedziczy od klasy Point i odpowiada za dzialanie punktu prowadzacego do mrowiska
 */
public class ToNestPoint : Point
{
    void Update()
    {
        pointTimeLeft -= Time.deltaTime;
        pointStrength -= scale;
        if (pointTimeLeft < 0)
        {
            gameObject.SetActive(false);
        }
        spriteRenderer.color -= new Color(0f, 0f, 0f, Time.deltaTime / pointTime);
    }

    protected override float FindDistanceToSource(Transform source)
    {
        return Vector2.Distance(transform.position, source.transform.position);
    }
}
