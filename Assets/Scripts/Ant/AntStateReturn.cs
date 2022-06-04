using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * klasa AntStateReturn dziedziczy od AntState i odpowiada za zachowanie mrowki w stanie wracania z jedzeniem do mrowiska
 */
public class AntStateReturn : AntState
{
    // obrot mrowki
    public override void Turn(float turnAngle, Transform target)
    {
        Vector2 desiredPos = (Vector2)target.position + (Random.insideUnitCircle * turnAngle / 2);
        float angle = Mathf.Atan2(desiredPos.y - transform.position.y, desiredPos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    // pozostawienie punktu
    public override void LeavePoint(Vector2 position, Transform source)
    {
        // jezeli jakis punkt jest blisko to punkty lacza sie w jeden
        foreach (GameObject point in ObjectPooling.pooledToFoodPoints)
        {
            if (Vector2.Distance(transform.position, point.transform.position) < mergePointsRadius && point.activeInHierarchy)
            {
                ToFoodPoint pointScript = point.GetComponent<ToFoodPoint>();
                pointScript.pointStrength += 1 / pointScript.distanceToSource;
                return;
            }
        }
        GameObject temp = Nest.objectPooling.GetToFoodPoint();
        temp.transform.position = position;
        temp.GetComponent<Point>().source = source;
        temp.GetComponent<Point>().OnCreate();
        temp.SetActive(true);
    }
}
