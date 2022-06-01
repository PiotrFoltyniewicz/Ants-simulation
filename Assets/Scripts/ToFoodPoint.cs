using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToFoodPoint : Point
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
