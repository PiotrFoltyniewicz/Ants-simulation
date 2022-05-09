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
        distanceToNest = FindDistanceToNest();
        pointTimeLeft = pointTime;
    }
        void Update()
    {
        pointTimeLeft -= Time.deltaTime;
        if(pointTimeLeft < 0)
        {
            Disappear();
        }
     //   spriteRenderer.color -= new Color(0f, 0f, 0f, 0.001f);
    }
    protected override float FindDistanceToNest()
    {
        return Vector2.Distance(transform.position, GameObject.Find("Nest").transform.position);
    }
}