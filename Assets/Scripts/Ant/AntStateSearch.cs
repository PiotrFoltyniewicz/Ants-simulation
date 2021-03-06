using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * klasa AntStateSearch dziedziczy od AntState i odpowiada za zachowanie mrowki w stanie szukania jedzenia lub punktow prowadzacych do jedzenia
 */
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
        // jezeli jakis punkt jest blisko to punkty lacza sie w jeden
        foreach (GameObject point in ObjectPooling.pooledToNestPoints)
        {
            if (Vector2.Distance(transform.position, point.transform.position) < mergePointsRadius && point.activeInHierarchy)
            {
                ToNestPoint pointScript = point.GetComponent<ToNestPoint>();
                pointScript.pointStrength += 1 / pointScript.distanceToSource;
                return;
            }
        }
        GameObject temp = Nest.objectPooling.GetToNestPoint();
        temp.transform.position = position;
        temp.GetComponent<Point>().source = source;
        temp.GetComponent<Point>().OnCreate();
        temp.SetActive(true);
    }

}
