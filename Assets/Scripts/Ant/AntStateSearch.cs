using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntStateSearch : AntState
{
    // obrot mrowki 
    public override void Turn(float turnAngle, Transform target)
    {
        Vector2 desiredPos = (Vector2)(transform.position + transform.right) + (Random.insideUnitCircle * turnAngle);
        float angle = Mathf.Atan2(desiredPos.y - transform.position.y, desiredPos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    // pozostawienie punktu
    public override void LeavePoint(Vector2 position, Transform source)
    {
        GameObject temp = Instantiate(pointObject,position, Quaternion.identity, null);
        temp.GetComponent<Point>().source = source;
        temp.SetActive(true);
        Nest.toNestList.Add(temp.transform);

    }

}
