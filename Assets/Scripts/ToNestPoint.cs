using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToNestPoint : Point
{
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        distanceToSource = FindDistanceToSource(source);
        pointTimeLeft = pointTime;
        pointStrength = 1/distanceToSource;
        SetStartingValues();
    }
    void FixedUpdate()
    {
        pointTimeLeft -= Time.fixedDeltaTime;
        if (pointTimeLeft < 0)
        {
            Disappear();
        }
        spriteRenderer.color -= new Color(0f, 0f, 0f, fade);
    }
    protected override float FindDistanceToSource(Transform source)
    {
        return Vector2.Distance(transform.position, source.transform.position);
    }
}
