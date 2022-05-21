using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntStateFollow : AntState
{
    public override void Turn(float turnAngle, Transform target)
    {
        Vector2 desiredPos = (Vector2)target.position + (Random.insideUnitCircle * turnAngle);
        float angle = Mathf.Atan2(desiredPos.y - transform.position.y, desiredPos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    // pozostawienie punktu
    public override void LeavePoint(Vector2 position)
    {
        GameObject temp = Instantiate(pointObject,position, Quaternion.identity, null);
        temp.SetActive(true);
    }
}
