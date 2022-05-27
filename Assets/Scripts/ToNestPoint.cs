using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToNestPoint : Point
{
    void FixedUpdate()
    {
        pointTimeLeft -= Time.fixedDeltaTime;

        if (pointTimeLeft < 0)
        {
            gameObject.SetActive(false);
        }
        spriteRenderer.color -= new Color(0f, 0f, 0f, fade);
    }
    protected override float FindDistanceToSource(Transform source)
    {
        return Vector2.Distance(transform.position, source.transform.position);
    }
}
